using RabbitMQ.Client;

namespace Api.Services;

public record QueueMessage() { };
public interface IQueueService
{
    public Task Send(QueueMessage message);
}

public class QueueService : IQueueService
{
    private Task<IConnection> _connection;
    private static readonly string queName = "subscription-queue";
    private static readonly string exchangeName = "subscription-exchange";
    private static readonly string routingKey = "subscription-exchange";
    private static string _uri;
    public QueueService(string rabbitMqUri)
    {
        _uri = rabbitMqUri;
    }

    public async Task Send(QueueMessage message)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(_uri)
        };

        _connection = factory.CreateConnectionAsync();
        var channel = await (await _connection).CreateChannelAsync();
        await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct);
        await channel.QueueDeclareAsync(queName, false, false, false, null);
        await channel.QueueBindAsync(queName, exchangeName, routingKey, null);
        
            
        byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(message.ToString());
        
        var props = new BasicProperties();
        props.ContentType = "text/plain";
        props.DeliveryMode = DeliveryModes.Persistent;
        props.Headers = new Dictionary<string, object>()!;
        props.Headers.Add("type", "event");
        
        await channel.BasicPublishAsync(exchange : exchangeName, routingKey: routingKey, false, basicProperties: props, body: messageBodyBytes);
    }


    
}