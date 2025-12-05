using Clerk.Net.Client;
using Clerk.Net.Client.Models;
using Microsoft.Extensions.Caching.Memory;

namespace TwistedTaleweaver.Integration.Clerk.Users;

public interface IUsersClient
{
    Task<Dictionary<string, User>> GetUsersAsync(IEnumerable<string> userIds);
}

internal class UsersClient(
    ClerkApiClient clerkApiClient,
    IMemoryCache cache) : IUsersClient
{
    public async Task<Dictionary<string, User>> GetUsersAsync(IEnumerable<string> userIds)
    {
        var usersByIds = new Dictionary<string, User>();
        var uncachedUserIds = new List<string>();

        foreach (var userId in userIds)
        {
            if (cache.TryGetValue($"clerk-user-{userId}", out User? cachedUser) || cachedUser is null)
            {
                uncachedUserIds.Add(userId);
            }
            else
            {
                usersByIds[userId] = cachedUser;
            }
        }

        var loadedUsers = await clerkApiClient.Users.GetAsync(config =>
        {
            config.QueryParameters.UserId = uncachedUserIds.ToArray();
        }) ?? new List<User>();

        foreach (var loadedUser in loadedUsers)
        {
            usersByIds[loadedUser.Id!] = loadedUser;

            var entry = cache.CreateEntry($"clerk-user-{loadedUser.Id}");
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            entry.Value = loadedUser;
        }
        
        return usersByIds;
    }
}