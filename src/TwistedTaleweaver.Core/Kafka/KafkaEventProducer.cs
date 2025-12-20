using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using TwistedTaleweaver.Core.Kafka.Attributes;

namespace TwistedTaleweaver.Core.Kafka;

/// <summary>
/// Defines a service that publishes events to an event queue.
/// </summary>
public interface IKafkaEventProducer : IDisposable
{
    /// <summary>
    /// Publishes a Kafka event to the event queue.
    /// </summary>
    Task PublishAsync<T>(T @event) where T: class;
}

internal sealed class KafkaEventProducer : IKafkaEventProducer
{
    private readonly IProducer<string, string> _producer;

    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        Converters = { new JsonStringEnumConverter() }
    };

    public KafkaEventProducer(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Kafka")
                              ?? throw new InvalidOperationException("Kafka ConnectionString is not configured.");
        
        var config = new ProducerConfig
        {
            BootstrapServers = connectionString,
            Acks = Acks.All,
            MessageSendMaxRetries = 3,
            EnableIdempotence = true,
            RequestTimeoutMs = 10000
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public Task PublishAsync<T>(T @event) where T: class
    {
        var eventType = @event.GetType().FullName!;
        var message = new Message<string, string>
        {
            Value = JsonSerializer.Serialize(@event, _serializerOptions),
            Headers = new Headers
            {
                { KafkaConstants.EventTypeHeader, Encoding.UTF8.GetBytes(eventType) }
            }
        };
        
        var partitionKey = GetPartitionKey(@event);
        if (partitionKey is not null)
        {
            message.Key = partitionKey;
        }
        
        return _producer.ProduceAsync(eventType, message);
    }

    public void Dispose()
    {
        _producer.Dispose();
    }
    
    private string? GetPartitionKey<T>(T @event) where T: class
    {
        var prop = typeof(T).GetProperties()
            .FirstOrDefault(p => Attribute.IsDefined(p, typeof(PartitionKeyAttribute)));

        if (prop is null)
        {
            return null;
        }
        
        var propertyName = prop.Name;
        var propertyValue = prop.GetValue(@event)?.ToString();

        return propertyValue is null ? null : $"{propertyName}:{propertyValue}";
    }
}
