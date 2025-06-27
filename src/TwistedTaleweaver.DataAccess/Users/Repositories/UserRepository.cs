using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Common.Extensions;
using TwistedTaleweaver.DataAccess.Users.Entities;
using Dapper;
using Npgsql;

namespace TwistedTaleweaver.DataAccess.Users.Repositories;

public interface IUserRepository : IRepository
{
    /// <summary>
    /// Tries to retrieve user by ExternalUserId, creates new if one doesn't exist
    /// </summary>
    Task<User> GetOrCreateAsync(string externalUserId, NpgsqlTransaction? transaction = null);
}

internal class UserRepository(IDbConnectionFactory connectionFactory) : IUserRepository
{
    public async Task<User> GetOrCreateAsync(string externalUserId, NpgsqlTransaction? transaction = null)
    {
        return await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                INSERT INTO users (external_user_id)
                VALUES (@ExternalUserId)
                ON CONFLICT (external_user_id) DO NOTHING;
                
                SELECT user_id, external_user_id, created_at
                FROM users 
                WHERE external_user_id = @ExternalUserId";

            return await connection.QuerySingleAsync<User>(sql, new
            {
                ExternalUserId = externalUserId
            }, tx);
        }, transaction);
    }
}