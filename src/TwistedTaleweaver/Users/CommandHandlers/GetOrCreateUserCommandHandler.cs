using TwistedTaleweaver.Api.Users.Commands;
using TwistedTaleweaver.DataAccess.Users.Writers;

namespace TwistedTaleweaver.Users.CommandHandlers;

internal class GetOrCreateUserCommandHandler(IUsersWriter usersWriter) : IGetOrCreateUserCommandHandler
{
    public async Task<Guid> ExecuteAsync(GetOrCreateUserCommand command)
    {
        var userDto = await usersWriter.GetOrCreateAsync(command.ExternalUserId);

        return userDto.UserId;
    }
}