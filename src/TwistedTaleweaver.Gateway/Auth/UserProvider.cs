using System.Security.Claims;
using TwistedTaleweaver.Api.Users.Commands;

namespace TwistedTaleweaver.Gateway.Auth;

public interface IUserProvider
{
    Task<Guid> GetUserIdAsync();
}

internal class UserIdProvider(
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

    private Guid? _userId;

    public async Task<Guid> GetUserIdAsync()
    {
        return _userId ??= await getOrCreateUserCommandHandler.Value
            .ExecuteAsync(new GetOrCreateUserCommand() { ExternalUserId = ExternalUserId });
    }
}