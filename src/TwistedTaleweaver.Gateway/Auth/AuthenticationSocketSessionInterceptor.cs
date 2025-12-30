using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Subscriptions;
using HotChocolate.AspNetCore.Subscriptions.Protocols;
using Microsoft.AspNetCore.Authentication;

namespace TwistedTaleweaver.Gateway.Auth;

public sealed class AuthenticationSocketSessionInterceptor
    : DefaultSocketSessionInterceptor
{
    public const string WEBSOCKET_TOKEN = "websocket-auth-token";
    public const string WEBOCKET_PAYLOAD_AUTH_KEY = "authorization";

    /// <summary>
    /// Called when the client sends the 'connection_init' message.
    /// We check the payload for a token, store it, and re-run the authentication.
    /// </summary>
    public override async ValueTask<ConnectionStatus> OnConnectAsync(
        ISocketSession session,
        IOperationMessagePayload connectionInitMessage,
        CancellationToken cancellationToken = new())
    {
        var context = session.Connection.HttpContext;

        var provider = context.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();

        var properties = connectionInitMessage.As<Dictionary<string, string?>>();

        if (properties is null)
        {
            return ConnectionStatus.Reject("Invalid connection init payload.");
        }

        if (properties.TryGetValue(WEBOCKET_PAYLOAD_AUTH_KEY, out string? token) &&
            token is { } stringToken)
        {
            context.Items[WEBSOCKET_TOKEN] = stringToken.StartsWith("Bearer ")
                ? stringToken["Bearer ".Length..]
                : stringToken;

            var defaultAuthenticate = await provider.GetDefaultAuthenticateSchemeAsync();
            if (defaultAuthenticate is not null)
            {
                var result = await context.AuthenticateAsync(defaultAuthenticate.Name);

                if (result.Principal is not null)
                {
                    context.User = result.Principal;
                    return ConnectionStatus.Accept();
                }
            }
        }

        return ConnectionStatus.Reject();
    }
}