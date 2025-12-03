using System.Security.Claims;

namespace TwistedTaleweaver.Gateway.Authorization;

public interface IUserProvider
{
    string UserId { get; }
}

internal class UserProvider(IHttpContextAccessor httpContextAccessor) : IUserProvider
{
    private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;
    private bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

    public string UserId
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

            var userId = userIdClaim.Value;

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new UnauthorizedAccessException("User ID claim is empty.");
            }

            return userId;
        }
    }
}