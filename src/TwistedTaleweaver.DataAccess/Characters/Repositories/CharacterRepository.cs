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
                    character_id,
                    user_id,
                    broadcaster_user_id,
                    created_at,
                    updated_at
                FROM characters 
                WHERE user_id = @UserId 
                  AND broadcaster_user_id = @BroadcasterUserId";

            return await connection.QuerySingleAsync<Character>(sql, new
            {
                UserId = userId,
                BroadcasterUserId = broadcasterUserId
            }, tx);
        }, transaction);
    }
}