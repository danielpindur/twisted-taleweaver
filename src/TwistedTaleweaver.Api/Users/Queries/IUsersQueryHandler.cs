using TwistedTaleweaver.Core.Queries;
using TwistedTaleweaver.Domain.Users;

namespace TwistedTaleweaver.Api.Users.Queries;

public class UsersQuery
{
    public required List<Guid> UserIds { get; set; }
}

public interface IUsersQueryHandler : IQueryHandler<UsersQuery, IEnumerable<User>>
{
}