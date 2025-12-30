using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace TwistedTaleweaver.Gateway.Auth;

public sealed class SkipWebSocketAuthenticationHandler :
    AuthenticationHandler<AuthenticationSchemeOptions>
{
    [Obsolete("Obsolete")]
    public SkipWebSocketAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }

    public SkipWebSocketAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder) : base(options, logger, encoder)
    {
    }

    /// <summary>
    /// This method is called to handle authentication.
    /// If it's a WebSocket upgrade request, we do nothing (NoResult).
    /// Otherwise, we fail the authentication.
    /// </summary>
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // If the request is a WebSocket upgrade request,
        // return "NoResult" to skip normal authentication checks.
        if (Context.WebSockets.IsWebSocketRequest)
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        // Otherwise, we fail authentication for non-WebSocket requests.
        return Task.FromResult(AuthenticateResult.Fail("Authentication not applicable"));
    }
}