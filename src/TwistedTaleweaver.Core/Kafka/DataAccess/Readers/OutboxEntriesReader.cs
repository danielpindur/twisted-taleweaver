using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Kafka.DataAccess.Dtos;

namespace TwistedTaleweaver.Core.Kafka.DataAccess.Readers;

internal interface IOutboxEntriesReader
{
    Task<IEnumerable<OutboxEntryDto>> GetPendingAsync();
}

internal class OutboxEntriesReader(IUnitOfWorkProvider provider) : DatabaseAccessor(provider), IOutboxEntriesReader
{
    public Task<IEnumerable<OutboxEntryDto>> GetPendingAsync()
    {
        const string sql = @"
            SELECT
                outbox_entry_id,
                created_at,
                topic,
                partition_key,
                headers,
                payload,
                processed_at,
                remaining_attempts
            FROM outbox_entries
            WHERE processed_at IS NULL AND remaining_attempts > 0";

        return QueryAsync<OutboxEntryDto>(sql);
    }
}