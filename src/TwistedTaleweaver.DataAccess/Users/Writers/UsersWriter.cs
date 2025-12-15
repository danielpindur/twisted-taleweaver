using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Users.Dtos;

namespace TwistedTaleweaver.DataAccess.Users.Writers;

public interface IUsersWriter
{
    Task<UserDto> GetOrCreateAsync(string externalUserId);
}

internal class UsersWriter(IUnitOfWorkProvider unitOfWorkProvider) : 
    DatabaseAccessor(unitOfWorkProvider), IUsersWriter
{
    public async Task<UserDto> GetOrCreateAsync(string externalUserId)
    {
        const string sql = @"
            INSERT INTO users (external_user_id)
            VALUES (@ExternalUserId)
            ON CONFLICT (external_user_id) DO UPDATE
                SET external_user_id = EXCLUDED.external_user_id
            RETURNING user_id, external_user_id";

        return await ExecuteWithOutputAsync<UserDto>(sql, new
        { 
            ExternalUserId = externalUserId
        });
    }
}