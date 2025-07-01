using Dapper;
using Npgsql;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Common.Extensions;
using TwistedTaleweaver.DataAccess.Permissions.Entities.Enums;

namespace TwistedTaleweaver.DataAccess.Permissions.Repositories;

public interface IUserBroadcasterPermissionRepository : IRepository
{
    /// <summary>
    /// Checks if a user has an active permission for a broadcaster.
    /// </summary>
    Task<bool> HasActivePermissionAsync(
        Guid userId,
        Guid broadcasterUserId,
        PermissionType permissionType,
        NpgsqlTransaction? transaction = null);
}

internal class UserBroadcasterPermissionRepository(IDbConnectionFactory connectionFactory) : IUserBroadcasterPermissionRepository
{
    public async Task<bool> HasActivePermissionAsync(
        Guid userId,
        Guid broadcasterUserId,
        PermissionType permissionType,
        NpgsqlTransaction? transaction = null)
    {
        return await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                SELECT EXISTS(
                    SELECT 1 FROM user_broadcaster_permissions 
                    WHERE user_id = @UserId 
                      AND broadcaster_user_id = @BroadcasterUserId 
                      AND permission_type_id = @PermissionTypeId
                      AND is_active = true
                )";

            return await connection.QuerySingleAsync<bool>(sql, new
            {
                UserId = userId,
                BroadcasterUserId = broadcasterUserId,
                PermissionTypeId = (byte)permissionType
            }, tx);
        }, transaction);
    }
}