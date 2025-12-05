using System.Security.Claims;
using TwistedTaleweaver.Users.CommandHandlers;
using TwistedTaleweaver.Users.Entities;

namespace TwistedTaleweaver.Gateway.Authorization;

public interface IUserProvider
{
    Task<User> GetUserAsync();
}

internal class UserProvider(
    IHttpContextAccessor httpContextAccessor,
    Lazy<IGetOrCreateUserCommandHandler> getOrCreateUserCommandHandler) : IUserProvider
{
    private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;
    
    private bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

    private string ExternalUserId
    {
        get
        {
            if (!IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var userIdClaim = User?.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
            {
                throw new UnauthorizedAccessException("User ID claim not found.");
            }

            var externalUserId = userIdClaim.Value;

            if (string.IsNullOrWhiteSpace(externalUserId))
            {
                throw new UnauthorizedAccessException("User ID claim is empty.");
            }

            return externalUserId;
        }
    }
    
    public async Task<User> GetUserAsync()
    {
        return await getOrCreateUserCommandHandler.Value
            .ExecuteAsync(new GetOrCreateUserCommand() { ExternalUserId = ExternalUserId });
    }
}