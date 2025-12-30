using TwistedTaleweaver.Api.Users.Entities;
using UserContract = TwistedTaleweaver.Gateway.Users.Contracts.User;

namespace TwistedTaleweaver.Gateway.Users.Mappers;

internal static class UserMapper
{
    public static UserContract ToContract(this User source)
    {
        return new UserContract()
        {
            Id = source.UserId,
            ExternalId = source.ExternalUserId,
            Name = source.Name,
        };
    }
}