namespace TwistedTaleweaver.Core.Kafka.Extensions;

/// <summary>
/// Provides extension methods for KafkaEvent.
/// </summary>
public static class KafkaEventExtensions
{
    /// <summary>
    /// Creates a new instance of the event with the specified payload.
    /// </summary>
    public static TE WithPayload<TP, TE>(this TE @event, TP payload) 
        where TP : EventPayload 
        where TE : KafkaEvent
    {
        if (payload.GetType() != @event.PayloadType)
        {
            throw new ArgumentException($"Payload type mismatch. Expected {@event.PayloadType}, got {payload.GetType()}");
        }

        var clonedEvent = Clone(@event);
        clonedEvent.AddPayload(payload);

        return clonedEvent;
    }

    /// <summary>
    /// Clones the current KafkaEvent instance, creating a new instance with the same event type, topic, and payload type.
    /// </summary>
    private static T Clone<T>(this T @event) where T : KafkaEvent
    {
        var type = typeof(T);

        return (T)(Activator.CreateInstance(type, @event.EventType, @event.Topic, @event.PayloadType)
                   ?? throw new InvalidOperationException($"Unable to create instance of {type}"));
    }
}