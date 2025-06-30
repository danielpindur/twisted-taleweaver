using System.Text;
using System.Text.Json;
using TwistedTaleweaver.Core.Kafka.Attributes;
using TwistedTaleweaver.Core.Kafka.Constants;
using Confluent.Kafka;

namespace TwistedTaleweaver.Core.Kafka;

/// <summary>
/// Base class for Kafka events.
/// </summary>
public abstract class KafkaEvent(string eventType, string topic, Type payloadType)
{
    private EventPayload? _eventPayload;

    /// <summary>
    /// The name of the event type.
    /// </summary>
    public string EventType { get; } = eventType;

    /// <summary>
    /// The topic to which the event belongs.
    /// </summary>
    public string Topic { get; } = topic;

    /// <summary>
    /// The type of the payload associated with the event.
    /// </summary>
    public Type PayloadType { get; } = payloadType;

    /// <summary>
    /// Gets the event payload associated with this event.
    /// </summary>
    public EventPayload Payload => _eventPayload ?? throw new InvalidOperationException("Event payload is not set.");

    /// <summary>
    /// Gets the partition key for the event based on the property marked with the PartitionKeyAttribute.
    /// If no such property exists, returns null.
    /// </summary>
    private string? GetPartitionKey()
    {
        var prop = PayloadType.GetProperties()
            .FirstOrDefault(p => Attribute.IsDefined(p, typeof(PartitionKeyAttribute)));

        if (prop is null)
        {
            return null;
        }
        
        var propertyName = prop.Name;
        var propertyValue = prop.GetValue(_eventPayload)?.ToString();

        return propertyValue is null ? null : $"{propertyName}:{propertyValue}";
    }

    /// <summary>
    /// Sets the event payload.
    /// </summary>
    public void AddPayload(EventPayload payload)
    {
        if (_eventPayload is not null)
        {
            throw new InvalidOperationException("Event payload is already set.");
        }

        if (payload.GetType() != PayloadType)
        {
            throw new ArgumentException($"Payload type '{payload.GetType()}' does not match expected type '{PayloadType}'.", nameof(payload));
        }

        _eventPayload = payload;
    }

    /// <summary>
    /// Gets the header for the event type, which is used in Kafka messages.
    /// </summary>
    private byte[] GetTypeHeader()
    {
        return Encoding.UTF8.GetBytes($"{GetType().Name}.{EventType}");
    }

    /// <summary>
    /// Converts the event to a Kafka message with the serialized payload and headers.
    /// The partition key is derived from the event's properties marked with the PartitionKeyAttribute.
    /// </summary>
    public Message<string, string> ToMessage()
    {
        return new Message<string, string>
        {
            Key = GetPartitionKey()!,
            Value = Serialize(),
            Headers = new Headers
            {
                { KafkaConstants.EventTypeHeader, GetTypeHeader() }
            }
        };
    }

    /// <summary>
    /// Serializes the event payload to a JSON string.
    /// </summary>
    private string Serialize()
    {
        if (_eventPayload is null)
        {
            throw new InvalidOperationException("Event payload is not set.");
        }

        return JsonSerializer.Serialize(_eventPayload, PayloadType);
    }
}