using System.Text;
using System.Text.Json;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

namespace Database;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private static readonly string queName = "subscription-queue";
    private static readonly string exchangeName = "subscription-exchange";
    private static readonly string routingKey = "subscription-exchange";
    private Task<IConnection> _connection;
    private IChannel _channel;
    private string rabbitMqUri = "amqp://guest:guest@rabbit:5672";
    private readonly IServiceScopeFactory _scopeFactory;
    

    public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        while(!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(15000, stoppingToken);

            try
            {
                await PollOnQueue(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            
        }
        
    }

    private async Task PollOnQueue(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(rabbitMqUri)
        };

        var connection = await factory.CreateConnectionAsync(stoppingToken);
        
        _channel = await (connection).CreateChannelAsync(cancellationToken: stoppingToken);
        await _channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct, cancellationToken: stoppingToken);
        await _channel.QueueDeclareAsync(queName, false, false, false, null, cancellationToken: stoppingToken);
        await _channel.QueueBindAsync(queName, exchangeName, routingKey, null, cancellationToken: stoppingToken);
        _logger.LogInformation("Polling on queue at {time}", DateTimeOffset.Now);

        var result = await _channel.BasicGetAsync(queName, false, stoppingToken);
        if (result == null)
        {
            
        }
        else
        {
            var props = result.BasicProperties;
            ReadOnlyMemory<byte> body = result.Body;
            await _channel.BasicAckAsync(result.DeliveryTag, false, stoppingToken);

            var bodyDecoded = Encoding.UTF8.GetString(body.ToArray());
            object? typeName = null;
            props.Headers?.TryGetValue("type", out typeName);
            if (typeName != null)
            {
                var typeQueueMessage = Encoding.UTF8.GetString((typeName as byte[]).ToArray());

                var newValue = typeQueueMessage switch
                {
                    "event" => CreateEventForUser(bodyDecoded, stoppingToken),
                    _ => throw new NotImplementedException()
                };
                
            }
            
            _logger.LogInformation(bodyDecoded);

        }

        await _channel.DisposeAsync();
        
    }

    private async Task CreateEventForUser(string body, CancellationToken stoppingToken)
    {
        using (var scope = _scopeFactory.CreateScope())
        {

            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var eventAndUser = JsonSerializer.Deserialize<EventAndUser>(body);
            
            var targetEvent =
                dbContext.EventDataTable.FirstOrDefault(a => a.EventLocation == eventAndUser.SubscriptionLocation);

            if (targetEvent is null)
            {
                targetEvent = new Event
                {
                    EventLocation = eventAndUser.SubscriptionLocation,
                };
                dbContext.EventDataTable.Add(targetEvent);
                
            }

            await dbContext.SaveChangesAsync(stoppingToken);

            dbContext.UserEventDataTable.Add(new UserEvent
            {
                EventId = targetEvent.Id,
                UserId = eventAndUser.UserId
            });
        }
    }
    
}

public class EventAndUser
{
    public int UserId { get; set; }
    public string SubscriptionLocation { get; set; }
}