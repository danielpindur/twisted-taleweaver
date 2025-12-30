using TwistedTaleweaver.Api.Users.Entities;
using TwistedTaleweaver.Core.Queries;

namespace TwistedTaleweaver.Api.Users.Queries;

public class UsersQuery
{
    public required List<Guid> UserIds { get; set; }
}

public interface IUsersQueryHandler : IQueryHandler<UsersQuery, IEnumerable<User>>
{
}