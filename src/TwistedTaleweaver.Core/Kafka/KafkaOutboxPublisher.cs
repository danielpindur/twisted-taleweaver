using System.Text;
using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using TwistedTaleweaver.Core.Kafka.DataAccess.Dtos;
using TwistedTaleweaver.Core.Kafka.DataAccess.Readers;
using TwistedTaleweaver.Core.Kafka.DataAccess.Writers;

namespace TwistedTaleweaver.Core.Kafka;

internal class KafkaOutboxPublisher : BackgroundService
{
    private readonly IProducer<string, string> _producer;
    private readonly IOutboxEntriesReader _outboxEntriesReader;
    private readonly IOutboxEntriesWriter _outboxEntriesWriter;

    public KafkaOutboxPublisher(
        IConfiguration configuration,
        IOutboxEntriesReader outboxEntriesReader,
        IOutboxEntriesWriter outboxEntriesWriter)
    {
        _outboxEntriesReader = outboxEntriesReader;
        _outboxEntriesWriter = outboxEntriesWriter;
        var connectionString = configuration.GetConnectionString("Kafka")
                               ?? throw new InvalidOperationException("Kafka ConnectionString is not configured.");

        var config = new ProducerConfig
        {
            BootstrapServers = connectionString,
            Acks = Acks.All,
            MessageSendMaxRetries = 3,
            EnableIdempotence = true,
            RequestTimeoutMs = 500
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // TODO: This needs to be fixed for multiple gateway containers setup as we could end up publishing the same event multiple times
        
        while (!stoppingToken.IsCancellationRequested)
        {
            var pendingOutboxEntries = await _outboxEntriesReader.GetPendingAsync();
            
            await ProcessPendingOutboxEntries(pendingOutboxEntries);
            await Task.Delay(TimeSpan.FromMilliseconds(100), stoppingToken);
        }
    }

    private async Task ProcessPendingOutboxEntries(IEnumerable<OutboxEntryDto> pendingOutboxEntries)
    {
        var successfullyPublishedOutboxEntries = new List<Guid>();
        var failedPublishedOutboxEntries = new List<Guid>();

        foreach (var outboxEntry in pendingOutboxEntries)
        {
            var message = new Message<string, string>
            {
                Value = outboxEntry.Payload
            };

            if (outboxEntry.Headers is not null)
            {
                var headers = JsonSerializer.Deserialize<Dictionary<string, string>>(outboxEntry.Headers);

                if (headers is not null)
                {
                    message.Headers = new Headers();
                    foreach (var header in headers)
                    {
                        message.Headers.Add(header.Key, Encoding.UTF8.GetBytes(header.Value));
                    }
                }
            }
            
            if (outboxEntry.PartitionKey is not null)
            {
                message.Key = outboxEntry.PartitionKey;
            }

            try
            {
                await _producer.ProduceAsync(outboxEntry.Topic, message);
                successfullyPublishedOutboxEntries.Add(outboxEntry.OutboxEntryId);
            }
            catch
            {
                failedPublishedOutboxEntries.Add(outboxEntry.OutboxEntryId);
            }
        }

        if (successfullyPublishedOutboxEntries.Count > 0)
        {
            await _outboxEntriesWriter.SaveSuccessfulAttemptsAsync(successfullyPublishedOutboxEntries);
        }

        if (failedPublishedOutboxEntries.Count > 0)
        {
            await _outboxEntriesWriter.SaveFailedAttemptsAsync(failedPublishedOutboxEntries);
        }
    }

    public override void Dispose()
    {
        _producer.Dispose();
        base.Dispose();
    }
}