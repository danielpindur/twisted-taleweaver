using TwistedTaleweaver.DataAccess.Broadcasters.Entities;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Common.Extensions;
using Dapper;
using Npgsql;

namespace TwistedTaleweaver.DataAccess.Broadcasters.Repositories;

public interface IBroadcasterRepository : IRepository
{
    /// <summary>
    /// Gets or creates a broadcaster for the given user.
    /// </summary>
    Task<Broadcaster> GetOrCreateAsync(Guid userId, NpgsqlTransaction? transaction = null);
}

internal class BroadcasterRepository(IDbConnectionFactory connectionFactory) : IBroadcasterRepository
{
    public async Task<Broadcaster> GetOrCreateAsync(Guid userId, NpgsqlTransaction? transaction = null)
    {
        return await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                INSERT INTO broadcasters (user_id)  
                VALUES (@UserId)  
                ON CONFLICT (user_id) DO UPDATE SET user_id = EXCLUDED.user_id  
                RETURNING user_id";

            return await connection.QuerySingleAsync<Broadcaster>(sql, new
            {
                UserId = userId
            }, tx);
        }, transaction);
    }
}