using System.Text;
using System.Text.Json;
using TwistedTaleweaver.Core.Kafka.Constants;
using Confluent.Kafka;

namespace TwistedTaleweaver.Core.Kafka.Consumers;

/// <summary>
/// Interface for Kafka event consumers.
/// </summary>
public interface IKafkaEventConsumer
{
    /// <summary>
    /// Event that this consumer handles.
    /// </summary>
    public KafkaEvent Event { get; }

    /// <summary>
    /// Processes the provided Kafka message.
    /// </summary>
    Task ConsumeAsync(Message<string, string> message, CancellationToken cancellationToken);
}

/// <summary>
/// Base class for Kafka event consumers.
/// </summary>
public abstract class KafkaEventConsumer<TPayload> : IKafkaEventConsumer where TPayload : EventPayload
{
    /// <summary>
    /// Event that this consumer handles.
    /// </summary>
    public abstract KafkaEvent Event { get; }

    /// <summary>
    /// Consumes a Kafka message and processes it.
    /// </summary>
    public async Task ConsumeAsync(Message<string, string> message, CancellationToken cancellationToken)
    {
        ValidateTypeHeader(message);

        var payload = JsonSerializer.Deserialize<TPayload>(message.Value) 
                      ?? throw new JsonException("Failed to deserialize payload.");
        
        await HandleAsync(payload, cancellationToken);
    }

    /// <summary>
    /// Validate the type header is present and matches expected type
    /// </summary>
    private void ValidateTypeHeader(Message<string, string> message)
    {
        var header = message.Headers.GetLastBytes(KafkaConstants.EventTypeHeader);
        if (header is null)
        {
            throw new InvalidOperationException("Missing event type header.");
        }

        var eventTypeHeaderValue = Encoding.UTF8.GetString(header);
        if (!eventTypeHeaderValue.Contains(Event.EventType))
        {
            throw new InvalidOperationException($"Message header '{eventTypeHeaderValue}' does not match expected event type '{Event.EventType}'");
        }
    }

    /// <summary>
    /// Concrete consumers implement this to perform actual processing on the payload.
    /// </summary>
    protected abstract Task HandleAsync(TPayload payload, CancellationToken cancellationToken);
}