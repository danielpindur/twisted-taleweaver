using System.Text.Json;
using System.Text.Json.Serialization;
using TwistedTaleweaver.Core.Kafka.Attributes;
using TwistedTaleweaver.Core.Kafka.DataAccess.Dtos;
using TwistedTaleweaver.Core.Kafka.DataAccess.Writers;

namespace TwistedTaleweaver.Core.Kafka;

/// <summary>
/// Defines a service that publishes events to an event queue.
/// </summary>
public interface IKafkaEventPublisher
{
    /// <summary>
    /// Publishes a Kafka event to the event queue.
    /// </summary>
    Task PublishAsync<T>(T @event) where T : class;
}

internal sealed class KafkaEventPublisher(IOutboxEntriesWriter outboxEntriesWriter) : IKafkaEventPublisher
{
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        Converters = { new JsonStringEnumConverter() }
    };

    public Task PublishAsync<T>(T @event) where T : class
    {
        var eventType = @event.GetType().FullName!;
        var partitionKey = GetPartitionKey(@event);
        var headers = new Dictionary<string, string>()
        {
            { KafkaConstants.EventTypeHeader, eventType }
        };

        var outboxEntry = new OutboxEntryDto()
        {
            Topic = @event.GetType().FullName!,
            Payload = JsonSerializer.Serialize(@event, _serializerOptions),
            Headers = JsonSerializer.Serialize(headers, _serializerOptions),
            PartitionKey = partitionKey,
            RemainingAttempts = 3
        };

        return outboxEntriesWriter.CreateAsync(outboxEntry);
    }

    private string? GetPartitionKey<T>(T @event) where T : class
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
