using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Users.Dtos;

namespace TwistedTaleweaver.DataAccess.Users.Readers;

public interface IUsersReader
{
    Task<IEnumerable<UserDto>> GetUsers(List<Guid> userIds);
}

internal class UsersReader(IUnitOfWorkProvider provider) : DatabaseAccessor(provider), IUsersReader
{
    public Task<IEnumerable<UserDto>> GetUsers(List<Guid> userIds)
    {
        const string sql = @"
            SELECT
                user_id,
                external_user_id
            FROM users
            WHERE user_id = ANY(@UserIds)";

        return QueryAsync<UserDto>(sql, new
        {
            UserIds = userIds
        });
    }
}