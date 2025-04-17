using System.Text;
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
//    private readonly ApplicationDbContext _dbContext;

    public Worker(ILogger<Worker> logger)//, ApplicationDbContext dbContext)
    {
        _logger = logger;
        //_dbContext = dbContext;
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
                    "event" => new Event(),
                    _ => throw new NotImplementedException()
                };
                
                //_dbContext.Add(newValue);

            }
            
            _logger.LogInformation(bodyDecoded);

        }

        await _channel.DisposeAsync();
        
        await Task.Delay(1000, stoppingToken);
    }
}