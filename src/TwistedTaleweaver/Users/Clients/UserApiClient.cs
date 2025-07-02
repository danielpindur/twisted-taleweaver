using Microsoft.Extensions.Caching.Distributed;
using TwistedTaleweaver.Common;
using TwistedTaleweaver.Integration.Twitch.Helix.Api;
using TwistedTaleweaver.Users.Entities;

namespace TwistedTaleweaver.Users.Clients;

internal interface IUserApiClient : IApiClient
{
    /// <summary>
    /// Resolves a list of external user IDs to their corresponding <see cref="ExternalUser"/> objects using cache.
    /// </summary>
    Task<Dictionary<string, ExternalUser>> ResolveUsersAsync(List<string> externalUserIds);
    
    /// <summary>
    /// Resolves an external user ID to it's corresponding <see cref="ExternalUser"/> objects using cache.
    /// </summary>
    Task<ExternalUser> ResolveUserAsync(string externalUserId);
}

internal class UserApiClient(
    IUsersApi usersApi,
    IDistributedCache cache,
    ILogger<UserApiClient> logger): IUserApiClient
{
    private const int UserCacheDurationInHours = 24;
    
    private static string GetExternalUserCacheKey(string userId) => $"twitch:userid:{userId}";

    public async Task<ExternalUser> ResolveUserAsync(string externalUserId)
    {
        return (await ResolveUsersAsync([externalUserId]))[externalUserId];
    }
    
    public async Task<Dictionary<string, ExternalUser>> ResolveUsersAsync(List<string> externalUserIds)
    {
        var result = new Dictionary<string, ExternalUser>();
        var toFetch = new List<string>();

        // TODO: This needs to be done in bulk and not user by user
        
        foreach (var externalUserId in externalUserIds.Distinct())
        {
            var cacheKey = GetExternalUserCacheKey(externalUserId);
            var cached = await cache.GetStringAsync(cacheKey);
            
            if (!string.IsNullOrEmpty(cached))
            {
                result[externalUserId] = new ExternalUser()
                {
                    ExternalUserId = externalUserId, 
                    Username = cached
                };
            }
            else
            {
                toFetch.Add(externalUserId);
            }
        }

        if (toFetch.Count == 0)
        {
            return result;
        }

        const int maxBatchSize = 100;
        var batches = toFetch.Chunk(maxBatchSize);

        foreach (var batch in batches)
        {
            var response = await usersApi.GetUsersAsync(id: batch.ToList());
            
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to resolve users: {response.RawContent}");
            }

            var resolvedUsers = response.Ok() ?? throw new HttpRequestException($"Failed to resolve users: {response.RawContent}");

            foreach (var user in resolvedUsers.Data)
            {
                result[user.Id] = new ExternalUser()
                {
                    ExternalUserId = user.Id, 
                    Username = user.DisplayName
                };

                try
                {
                    await cache.SetStringAsync(
                        GetExternalUserCacheKey(user.Id),
                        user.DisplayName,
                        new DistributedCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(UserCacheDurationInHours)
                        });
                }
                catch (Exception e)
                {
                    // We don't want to fail the entire batch if one user fails to cache since we already retrieved it
                    logger.LogError(e, "Failed to cache user {UserId} with username {Username}", user.Id, user.Login);
                }
            }
        }

        return result;
    }
}