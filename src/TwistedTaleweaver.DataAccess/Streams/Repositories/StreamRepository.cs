using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Common.Extensions;
using Dapper;
using Npgsql;
using Stream = TwistedTaleweaver.DataAccess.Streams.Entities.Stream;

namespace TwistedTaleweaver.DataAccess.Streams.Repositories;

public interface IStreamRepository : IRepository
{
    /// <summary>
    /// Gets the currently active stream for a broadcaster.
    /// </summary>
    Task<Stream?> GetActiveStreamAsync(Guid broadcasterId, NpgsqlTransaction? transaction = null);

    /// <summary>
    /// Starts a new stream for a broadcaster.
    /// </summary>    
    Task StartStreamAsync(Guid broadcasterId, string externalStreamId, DateTimeOffset startedAt, NpgsqlTransaction? transaction = null);
    
    /// <summary>
    /// Ends an active stream by setting its ended_at timestamp.
    /// </summary>
    Task EndStreamAsync(Guid streamId, NpgsqlTransaction? transaction = null);
}

internal class StreamRepository(IDbConnectionFactory connectionFactory) : IStreamRepository
{
    public async Task<Stream?> GetActiveStreamAsync(Guid broadcasterId, NpgsqlTransaction? transaction = null)
    {
        return await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                SELECT 
                    stream_id,
                    broadcaster_user_id,
                    external_stream_id,
                    started_at,
                    ended_at
                FROM streams 
                WHERE broadcaster_user_id = @BroadcasterUserId
                  AND ended_at IS NULL
                ORDER BY started_at DESC
                LIMIT 1";

            return await connection.QuerySingleOrDefaultAsync<Stream>(sql, new
            {
                BroadcasterUserId = broadcasterId
            }, tx);
        }, transaction);
    }

    public async Task StartStreamAsync(Guid broadcasterId, string externalStreamId, DateTimeOffset startedAt, NpgsqlTransaction? transaction = null)
    {
        await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                INSERT INTO streams
                (broadcaster_user_id, external_stream_id, started_at)
                VALUES
                (@BroadcasterUserId, @ExternalStreamId, @StartedAt)";

            await connection.ExecuteAsync(sql, new
            {
                BroadcasterUserId = broadcasterId,
                ExternalStreamId = externalStreamId,
                StartedAt = startedAt
            }, tx);
        }, transaction);
    }

    public async Task EndStreamAsync(Guid streamId, NpgsqlTransaction? transaction = null)
    {
        await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                UPDATE streams
                SET ended_at = @EndedAt
                WHERE stream_id = @StreamId AND ended_at IS NULL";

            await connection.ExecuteAsync(sql, new
            {
                StreamId = streamId,
                EndedAt = DateTimeOffset.UtcNow
            }, tx);
        }, transaction);
    }
}