using Dapper;
using Npgsql;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Common.Extensions;
using TwistedTaleweaver.DataAccess.Expeditions.Entities.Enums;

namespace TwistedTaleweaver.DataAccess.Expeditions.Repositories;

public interface IExpeditionRepository : IRepository
{
    /// <summary>
    /// Creates a new expedition for a stream.
    /// </summary>
    Task<Guid> CreateExpeditionAsync(
        Guid streamId, 
        Guid createdByUserId,
        NpgsqlTransaction? transaction = null);
    
    /// <summary>
    /// Checks if a broadcaster has an expedition in progress 
    /// (Created or Started status).
    /// </summary>
    Task<bool> HasExpeditionInProgressAsync(
        Guid broadcasterUserId,
        NpgsqlTransaction? transaction = null);
}

internal class ExpeditionRepository(IDbConnectionFactory connectionFactory) : IExpeditionRepository
{
    public async Task<Guid> CreateExpeditionAsync(
        Guid streamId, 
        Guid createdByUserId,
        NpgsqlTransaction? transaction = null)
    {
        return await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            var now = DateTimeOffset.UtcNow;
            
            const string sql = @"
                INSERT INTO expeditions (
                    stream_id, 
                    created_by_user_id, 
                    status_id, 
                    created_at, 
                    updated_at
                )
                VALUES (
                    @StreamId, 
                    @CreatedByUserId, 
                    @StatusId, 
                    @CreatedAt, 
                    @UpdatedAt
                )
                RETURNING expedition_id";

            return await connection.QuerySingleAsync<Guid>(sql, new
            {
                StreamId = streamId,
                CreatedByUserId = createdByUserId,
                StatusId = (byte)ExpeditionStatus.Created,
                CreatedAt = now,
                UpdatedAt = now
            }, tx);
        }, transaction);
    }
    
    public async Task<bool> HasExpeditionInProgressAsync(
        Guid broadcasterUserId,
        NpgsqlTransaction? transaction = null)
    {
        return await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                SELECT EXISTS(
                    SELECT 1 
                    FROM expeditions e
                    INNER JOIN streams s ON e.stream_id = s.stream_id
                    WHERE s.broadcaster_user_id = @BroadcasterUserId 
                      AND e.status_id IN (@Created, @Started)
                )";

            return await connection.QuerySingleAsync<bool>(sql, new
            {
                BroadcasterUserId = broadcasterUserId,
                Created = (byte)ExpeditionStatus.Created,
                Started = (byte)ExpeditionStatus.Started
            }, tx);
        }, transaction);
    }
}