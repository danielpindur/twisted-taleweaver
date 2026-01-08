using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Kafka.DataAccess.Dtos;

namespace TwistedTaleweaver.Core.Kafka.DataAccess.Writers;

internal interface IOutboxEntriesWriter
{
    Task CreateAsync(OutboxEntryDto outboxEntry);
    
    Task SaveFailedAttemptsAsync(List<Guid> outboxEntryIds);
    
    Task SaveSuccessfulAttemptsAsync(List<Guid> outboxEntryIds);
}

internal class OutboxEntriesWriter(IUnitOfWorkProvider provider) : DatabaseAccessor(provider), IOutboxEntriesWriter
{
    public Task CreateAsync(OutboxEntryDto outboxEntry)
    {
        const string sql = @"
            INSERT INTO outbox_entries 
            (topic, partition_key, headers, payload, remaining_attempts)
            VALUES
            (@Topic, @PartitionKey, @Headers, @Payload, @RemainingAttempts)";

        return ExecuteAsync(sql, outboxEntry);
    }

    public Task SaveFailedAttemptsAsync(List<Guid> outboxEntryIds)
    {
        const string sql = @"
            UPDATE outbox_entries 
            SET remaining_attempts = remaining_attempts - 1
            WHERE outbox_entry_id = ANY(@OutboxEntryIds)";

        return ExecuteAsync(sql, new { OutboxEntryIds = outboxEntryIds });
    }

    public Task SaveSuccessfulAttemptsAsync(List<Guid> outboxEntryIds)
    {
        const string sql = @"
            UPDATE outbox_entries 
            SET remaining_attempts = remaining_attempts - 1, processed_at = CURRENT_TIMESTAMP
            WHERE outbox_entry_id = ANY(@OutboxEntryIds)";

        return ExecuteAsync(sql, new { OutboxEntryIds = outboxEntryIds });
    }
}