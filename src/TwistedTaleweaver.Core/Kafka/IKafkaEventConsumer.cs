namespace TwistedTaleweaver.Core.Kafka;

/// <summary>
/// Interface for Kafka event consumer.
/// </summary>
public interface IKafkaEventConsumer
{
}

/// <summary>
/// Typed interface for Kafka event consumer.
/// </summary>
public interface IKafkaEventConsumer<in T> : IKafkaEventConsumer where T : class
{
    /// <summary>
    /// Consumes the provided Kafka event.
    /// </summary>
    Task ConsumeAsync(T @event, CancellationToken cancellationToken);
}
