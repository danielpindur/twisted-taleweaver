using TwistedTaleweaver.DataAccess.Users.Dtos;
using TwistedTaleweaver.Domain.Users;
using ClerkUser = Clerk.Net.Client.Models.User;

namespace TwistedTaleweaver.Users.Mappers;

public static class UsersMapper
{
    public static User ToUser(this UserDto source, Dictionary<string, ClerkUser> userByExternalId)
    {
        return new User()
        {
            UserId = source.UserId,
            ExternalUserId = source.ExternalUserId,
            Name = userByExternalId[source.ExternalUserId].Username!
        };
    }
}