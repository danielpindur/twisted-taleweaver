using System.Text;
using TwistedTaleweaver.Core.Kafka.Configuration;
using TwistedTaleweaver.Core.Kafka.Constants;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TwistedTaleweaver.Core.Kafka.Consumers;

/// <summary>
/// Dispatcher for Kafka consumers that listens to multiple topics and dispatches messages to the appropriate consumer.
/// </summary>
public class KafkaConsumerDispatcher : BackgroundService
{
    private readonly ILogger<KafkaConsumerDispatcher> _logger;
    private readonly IEnumerable<IKafkaEventConsumer> _consumers;
    private readonly IConsumer<string, string> _consumer;

    public KafkaConsumerDispatcher(
        ILogger<KafkaConsumerDispatcher> logger,
        IEnumerable<IKafkaEventConsumer> consumers,
        IOptions<KafkaConfiguration> kafkaConfiguration)
    {
        _logger = logger;
        _consumers = consumers;

        var config = new ConsumerConfig
        {
            BootstrapServers = kafkaConfiguration.Value.BootstrapServers,
            GroupId = kafkaConfiguration.Value.GroupId 
                      ?? throw new ArgumentNullException(nameof(kafkaConfiguration.Value.GroupId)),
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        _consumer = new ConsumerBuilder<string, string>(config).Build();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!_consumers.Any())
        {
            _logger.LogWarning("No Kafka consumers registered. The dispatcher will not start consuming messages.");
            _consumer.Close();
            return Task.CompletedTask;
        }
        
        Subscribe();

        return Task.Run(() => ConsumeLoop(stoppingToken), stoppingToken);
    }

    private void Subscribe()
    {
        var topics = _consumers.Select(c => c.Event.Topic).Distinct().ToList();

        _logger.LogInformation("Subscribing to topics: {topics}", string.Join(", ", topics));
        _consumer.Subscribe(topics);
    }

    private void ConsumeLoop(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer.Consume(stoppingToken);

                    if (result?.Message is null)
                    {
                        continue;
                    }
                    
                    _logger.LogInformation("{@KafkaEvent}", result);

                    DispatchMessage(result.Topic, result.Message, stoppingToken).GetAwaiter().GetResult();

                    _consumer.Commit(result);
                }
                catch (ConsumeException ex)
                {
                    _logger.LogError(ex, "Consume error: {error}", ex.Error.Reason);
                }
            }
        }
        catch (OperationCanceledException)
        {
            // Graceful shutdown.
        }
        finally
        {
            _consumer.Close();
        }
    }

    private async Task DispatchMessage(string topic, Message<string, string> message, CancellationToken cancellationToken)
    {
        var headerBytes = message.Headers.GetLastBytes(KafkaConstants.EventTypeHeader);
        if (headerBytes is null)
        {
            _logger.LogWarning("Event {@KafkaEvent} in topic {topic} does not have a valid event type header.", message, topic);
            return;
        }

        var eventTypeHeader = Encoding.UTF8.GetString(headerBytes);
        var matchingConsumers = _consumers
            .Where(c => c.Event.Topic == topic && eventTypeHeader.Contains(c.Event.EventType))
            .ToList();

        if (!matchingConsumers.Any())
        {
            _logger.LogWarning("No consumer registered for event type: {header}.", eventTypeHeader);
            return;
        }

        var consumingTasks = new List<Task>();
        
        foreach (var consumer in matchingConsumers)
        {
            try
            {
                consumingTasks.Add(consumer.ConsumeAsync(message, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error while processing message by consumer {consumer}",
                    consumer.GetType().Name);
            }
        }
        
        try
        {
            await Task.WhenAll(consumingTasks);
        }
        catch (Exception ex)
        {
            // If multiple tasks failed, the exception might be an AggregateException
            if (ex is AggregateException aggEx)
            {
                foreach (var inner in aggEx.Flatten().InnerExceptions)
                {
                    _logger.LogError(inner, "A consumer task failed processing message.");
                }
            }
            else
            {
                _logger.LogError(ex, "A consumer task failed processing message.");
            }
        }
    }
}