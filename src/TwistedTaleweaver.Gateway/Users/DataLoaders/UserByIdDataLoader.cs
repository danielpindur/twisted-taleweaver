using TwistedTaleweaver.Api.Users.Queries;
using TwistedTaleweaver.Gateway.Users.Contracts;
using TwistedTaleweaver.Gateway.Users.Mappers;

namespace TwistedTaleweaver.Gateway.Users.DataLoaders;

public class UserByIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    IUsersQueryHandler usersQueryHandler)
    : BatchDataLoader<Guid, User>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<Guid, User>> LoadBatchAsync(
        IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
    {
        var users = await usersQueryHandler.ExecuteAsync(new UsersQuery()
        {
            UserIds = keys.ToList()
        });

        return users.ToDictionary(x => x.UserId, y => y.ToContract());
    }
}