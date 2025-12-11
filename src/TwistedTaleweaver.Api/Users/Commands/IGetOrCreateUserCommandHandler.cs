using TwistedTaleweaver.Core.Commands;
using TwistedTaleweaver.Domain.Users;

namespace TwistedTaleweaver.Api.Users.Commands;

public class GetOrCreateUserCommand
{
    public required string ExternalUserId { get; set; }
}

public interface IGetOrCreateUserCommandHandler : ICommandHandler<GetOrCreateUserCommand, Guid>
{
}