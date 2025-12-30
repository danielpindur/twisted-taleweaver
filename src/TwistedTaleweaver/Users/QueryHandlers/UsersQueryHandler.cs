using TwistedTaleweaver.Api.Users.Entities;
using TwistedTaleweaver.Api.Users.Queries;
using TwistedTaleweaver.DataAccess.Users.Readers;
using TwistedTaleweaver.Integration.Clerk.Users;
using TwistedTaleweaver.Users.Mappers;

namespace TwistedTaleweaver.Users.QueryHandlers;

internal class UsersQueryHandler(
    IUsersReader usersReader,
    IUsersAdapter usersAdapter) : IUsersQueryHandler
{
    public async Task<IEnumerable<User>> ExecuteAsync(UsersQuery query)
    {
        var userDtos = (await usersReader.GetUsers(query.UserIds)).ToList();
        var userByExternalId = await usersAdapter.GetUsersAsync(userDtos.Select(x => x.ExternalUserId));

        return userDtos.ToUsers(userByExternalId);
    }
}