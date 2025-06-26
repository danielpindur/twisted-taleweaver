using TwistedTaleweaver.Core.Kafka.Configuration;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TwistedTaleweaver.Core.Kafka.Producers;

/// <summary>
/// Defines a service that publishes stream-related events to a message queue.
/// </summary>
public interface IKafkaEventProducer : IAsyncDisposable
{
    /// <summary>
    /// Publishes a Kafka event to the message queue.
    /// </summary>
    Task PublishAsync(KafkaEvent @event);
}

/// <summary>
/// Implements a Kafka-based message publisher for stream-related events.
/// </summary>
internal sealed class KafkaEventProducer : IKafkaEventProducer
{
    private readonly ILogger<KafkaEventProducer> _logger;
    private readonly IProducer<string, string> _producer;

    public KafkaEventProducer(
        ILogger<KafkaEventProducer> logger,
        IOptions<KafkaConfiguration> configuration)
    {
        _logger = logger;

        var config = new ProducerConfig
        {
            BootstrapServers = configuration.Value.BootstrapServers,
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task PublishAsync(KafkaEvent @event)
    {
        try
        {
            await _producer.ProduceAsync(@event.Topic, @event.ToMessage());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error publishing {EventType} event to Kafka", @event.EventType);
            throw;
        }
    }

    public ValueTask DisposeAsync()
    {
        _producer.Dispose();
        return ValueTask.CompletedTask;
    }
}
