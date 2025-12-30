using TwistedTaleweaver.Api.Users.Entities;
using TwistedTaleweaver.DataAccess.Users.Dtos;
using ClerkUser = Clerk.Net.Client.Models.User;

namespace TwistedTaleweaver.Users.Mappers;

internal static class UsersMapper
{
    public static IEnumerable<User> ToUsers(
        this IEnumerable<UserDto> source,
        Dictionary<string, ClerkUser> userByExternalId)
    {
        return source.Select(x => ToUser(x, userByExternalId));
    }
    
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