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
}