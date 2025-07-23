using Dapper;
using Npgsql;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Common.Extensions;
using TwistedTaleweaver.DataAccess.Expeditions.Entities;
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
    
    /// <summary>
    /// Returns last expedition for given broadcaster, if any.
    /// </summary>
    Task<Expedition?> GetLastExpeditionAsync(
        Guid broadcasterUserId, 
        NpgsqlTransaction? transaction = null);
    
    /// <summary>
    /// Adds a character to an expedition.
    /// </summary>
    Task JoinExpeditionAsync(
        Guid characterId,
        Guid expeditionId,
        NpgsqlTransaction? transaction = null);
    
    /// <summary>
    /// Checks if a character is already part of an expedition.
    /// </summary>
    Task<bool> IsCharacterInExpeditionAsync(
        Guid characterId,
        Guid expeditionId,
        NpgsqlTransaction? transaction = null);
    
    /// <summary>
    /// Gets expeditions by status for a specific broadcaster.
    /// </summary>
    Task<IEnumerable<Expedition>> GetByBroadcasterAndStatusAsync(
        Guid broadcasterUserId, 
        ExpeditionStatus status, 
        NpgsqlTransaction? transaction = null);
    
    /// <summary>
    /// Gets expeditions that are ready to start based on a waiting period.
    /// </summary>
    Task<IEnumerable<Expedition>> GetExpeditionsToStartAsync(
        TimeSpan waitingPeriod,
        NpgsqlTransaction? transaction = null);
    
    /// <summary>
    /// Updates the status of an expedition.
    /// </summary>
    Task UpdateExpeditionStatusAsync(
        Guid expeditionId,
        ExpeditionStatus oldStatus,
        ExpeditionStatus newStatus,
        NpgsqlTransaction? transaction = null);

    /// <summary>
    /// Checks if given expedition has any participants.
    /// </summary>
    Task<bool> HasParticipantsAsync(
        Guid expeditionId,
        NpgsqlTransaction? transaction = null);

    /// <summary>
    /// Get started expeditions to calculate the combat for.
    /// </summary>
    Task<IEnumerable<Expedition>> GetExpeditionsToCalculateAsync(NpgsqlTransaction? transaction = null);

    /// <summary>
    /// Gets the participants of an expedition.
    /// </summary>    
    Task<IEnumerable<ExpeditionParticipant>> GetParticipantsAsync(
        Guid expeditionId,
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

    public async Task<Expedition?> GetLastExpeditionAsync(
        Guid broadcasterUserId, 
        NpgsqlTransaction? transaction = null)
    {
        return await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                SELECT
                    e.expedition_id,
                    e.stream_id,
                    e.created_by_user_id,
                    e.status_id as Status,
                    e.created_at,
                    e.started_at,
                    e.completed_at,
                    e.failed_at,
                    e.updated_at
                FROM expeditions e
                INNER JOIN streams s ON e.stream_id = s.stream_id
                WHERE s.broadcaster_user_id = @BroadcasterUserId 
                ORDER BY e.created_at DESC
                LIMIT 1";

            return await connection.QuerySingleOrDefaultAsync<Expedition>(sql, new
            {
                BroadcasterUserId = broadcasterUserId
            }, tx);
        }, transaction);
    }

    public async Task JoinExpeditionAsync(
        Guid characterId,
        Guid expeditionId,
        NpgsqlTransaction? transaction = null)
    {
        await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                INSERT INTO character_expeditions (character_id, expedition_id)
                VALUES (@CharacterId, @ExpeditionId)
                ON CONFLICT (character_id, expedition_id) DO NOTHING";

            await connection.ExecuteAsync(sql, new
            {
                CharacterId = characterId,
                ExpeditionId = expeditionId
            }, tx);
        }, transaction);
    }
    
    public async Task<bool> IsCharacterInExpeditionAsync(
        Guid characterId,
        Guid expeditionId,
        NpgsqlTransaction? transaction = null)
    {
        return await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                SELECT EXISTS(
                    SELECT 1 FROM character_expeditions 
                    WHERE character_id = @CharacterId 
                      AND expedition_id = @ExpeditionId
                )";

            return await connection.QuerySingleAsync<bool>(sql, new
            {
                CharacterId = characterId,
                ExpeditionId = expeditionId
            }, tx);
        }, transaction);
    }
    
    public async Task<IEnumerable<Expedition>> GetByBroadcasterAndStatusAsync(
        Guid broadcasterUserId, 
        ExpeditionStatus status, 
        NpgsqlTransaction? transaction = null)
    {
        return await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                SELECT 
                    e.expedition_id,
                    e.stream_id,
                    e.created_by_user_id,
                    e.status_id as Status,
                    e.created_at,
                    e.started_at,
                    e.completed_at,
                    e.failed_at,
                    e.updated_at
                FROM expeditions e
                INNER JOIN streams s ON e.stream_id = s.stream_id
                WHERE s.broadcaster_user_id = @BroadcasterUserId 
                  AND e.status_id = @StatusId
                ORDER BY e.created_at DESC";

            return await connection.QueryAsync<Expedition>(sql, new
            {
                BroadcasterUserId = broadcasterUserId,
                StatusId = (byte)status
            }, tx);
        }, transaction);
    }

    public async Task<IEnumerable<Expedition>> GetExpeditionsToStartAsync(
        TimeSpan waitingPeriod,
        NpgsqlTransaction? transaction = null)
    {
        return await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                SELECT 
                    e.expedition_id,
                    e.stream_id,
                    e.created_by_user_id,
                    e.status_id as Status,
                    e.created_at,
                    e.started_at,
                    e.completed_at,
                    e.failed_at,
                    e.updated_at,
                    u.user_id AS BroadcasterUserId,
                    u.external_user_id AS BroadcasterExternalUserId
                FROM expeditions e
                INNER JOIN streams s ON e.stream_id = s.stream_id
                INNER JOIN broadcasters b ON s.broadcaster_user_id = b.user_id
                INNER JOIN users u ON u.user_id = b.user_id
                WHERE e.status_id = @CreatedStatusId
                  AND e.created_at <= @CutoffTime";

            var cutoffTime = DateTimeOffset.UtcNow - waitingPeriod;
        
            return await connection.QueryAsync<Expedition>(sql, new
            {
                CreatedStatusId = (byte)ExpeditionStatus.Created,
                CutoffTime = cutoffTime
            }, tx);
        }, transaction);
    }

    public async Task UpdateExpeditionStatusAsync(
        Guid expeditionId,
        ExpeditionStatus oldStatus,
        ExpeditionStatus newStatus,
        NpgsqlTransaction? transaction = null)
    {
        await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            var sql = newStatus switch
            {
                ExpeditionStatus.Started => @"
                    UPDATE expeditions 
                    SET status_id = @NewStatusId,
                        started_at = @Timestamp,
                        updated_at = @Timestamp
                    WHERE expedition_id = @ExpeditionId
                        AND status_id = @OldStatusId",
                ExpeditionStatus.Completed => @"
                    UPDATE expeditions 
                    SET status_id = @NewStatusId,
                        completed_at = @Timestamp,
                        updated_at = @Timestamp
                    WHERE expedition_id = @ExpeditionId
                        AND status_id = @OldStatusId",
                ExpeditionStatus.Failed => @"
                    UPDATE expeditions 
                    SET status_id = @NewStatusId,
                        failed_at = @Timestamp,
                        updated_at = @Timestamp
                    WHERE expedition_id = @ExpeditionId
                        AND status_id = @OldStatusId",
                ExpeditionStatus.Cancelled => @"
                    UPDATE expeditions 
                    SET status_id = @NewStatusId,
                        cancelled_at = @Timestamp,
                        updated_at = @Timestamp
                    WHERE expedition_id = @ExpeditionId
                        AND status_id = @OldStatusId",
                _ => throw new ArgumentException($"Unsupported status transition: {newStatus.ToString()}")
            };

            var rowsAffected = await connection.ExecuteAsync(sql, new
            {
                ExpeditionId = expeditionId,
                NewStatusId = (byte)newStatus,
                OldStatusId = (byte)oldStatus,
                Timestamp = DateTimeOffset.UtcNow
            }, tx);
        
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"Failed to update expedition {expeditionId} status from {oldStatus} to {newStatus}.");
            }
        }, transaction);
    }

    public async Task<bool> HasParticipantsAsync(Guid expeditionId, NpgsqlTransaction? transaction = null)
    {
        return await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            var sql = @"
                SELECT EXISTS (
                    SELECT 1
                    FROM character_expeditions ce
                    WHERE ce.expedition_id = @ExpeditionId)";
            
            return await connection.QuerySingleAsync<bool>(sql, new
            {
                ExpeditionId = expeditionId
            }, tx);
        }, transaction);
    }

    public async Task<IEnumerable<Expedition>> GetExpeditionsToCalculateAsync(NpgsqlTransaction? transaction = null)
    {
        return await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                SELECT 
                    e.expedition_id,
                    e.stream_id,
                    e.created_by_user_id,
                    e.status_id as Status,
                    e.created_at,
                    e.started_at,
                    e.completed_at,
                    e.failed_at,
                    e.updated_at,
                    u.user_id AS BroadcasterUserId,
                    u.external_user_id AS BroadcasterExternalUserId
                FROM expeditions e
                INNER JOIN streams s ON e.stream_id = s.stream_id
                INNER JOIN broadcasters b ON s.broadcaster_user_id = b.user_id
                INNER JOIN users u ON u.user_id = b.user_id
                WHERE e.status_id = @StartedStatusId";

            return await connection.QueryAsync<Expedition>(sql, new
            {
                StartedStatusId = (byte)ExpeditionStatus.Started
            }, tx);
        }, transaction);
    }

    public async Task<IEnumerable<ExpeditionParticipant>> GetParticipantsAsync(Guid expeditionId, NpgsqlTransaction? transaction = null)
    {
        return await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                SELECT 
                    ce.character_id,
                    ce.expedition_id,
                    u.user_id,
                    u.external_user_id
                FROM character_expeditions ce
                INNER JOIN characters c ON ce.character_id = c.character_id
                INNER JOIN users u ON u.user_id = c.user_id
                WHERE ce.expedition_id = @ExpeditionId";

            return await connection.QueryAsync<ExpeditionParticipant>(sql, new
            {
                ExpeditionId = expeditionId
            }, tx);
        }, transaction);
    }
}