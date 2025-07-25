using Dapper;
using Npgsql;
using TwistedTaleweaver.DataAccess.Characters.Entities;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Common.Extensions;

namespace TwistedTaleweaver.DataAccess.Characters.Repositories;

public interface ICharacterRepository : IRepository
{
    /// <summary>
    /// Gets or creates an active character for a user under a specific broadcaster.
    /// </summary>
    Task<Character> GetOrCreateAsync(
        Guid userId, 
        Guid broadcasterUserId, 
        NpgsqlTransaction? transaction = null);

    /// <summary>
    /// Adds experience increments to characters.
    /// </summary>
    Task AddExperienceIncrementsAsync(
        List<CharacterExperienceIncrement> experiences,
        NpgsqlTransaction? transaction = null);
}

internal class CharacterRepository(IDbConnectionFactory connectionFactory) : ICharacterRepository
{
    public async Task<Character> GetOrCreateAsync(
        Guid userId, 
        Guid broadcasterUserId, 
        NpgsqlTransaction? transaction = null)
    {
        return await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                INSERT INTO characters (user_id, broadcaster_user_id)
                SELECT @UserId, @BroadcasterUserId
                WHERE NOT EXISTS (
                    SELECT 1 FROM characters 
                    WHERE user_id = @UserId 
                      AND broadcaster_user_id = @BroadcasterUserId 
                );
                
                SELECT 
                    c.character_id,
                    c.user_id,
                    u.external_user_id,
                    c.broadcaster_user_id,
                    c.created_at,
                    c.updated_at
                FROM characters c
                INNER JOIN users u ON c.user_id = u.user_id
                WHERE c.user_id = @UserId 
                  AND c.broadcaster_user_id = @BroadcasterUserId";

            return await connection.QuerySingleAsync<Character>(sql, new
            {
                UserId = userId,
                BroadcasterUserId = broadcasterUserId
            }, tx);
        }, transaction);
    }

    public async Task AddExperienceIncrementsAsync(
        List<CharacterExperienceIncrement> experiences, 
        NpgsqlTransaction? transaction = null)
    {
        await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                INSERT INTO character_experience_increments 
                (character_id, amount, expedition_id)
                VALUES (@CharacterId, @Amount, @ExpeditionId)";

            await connection.ExecuteAsync(sql, experiences, tx);
        }, transaction);
    }
}