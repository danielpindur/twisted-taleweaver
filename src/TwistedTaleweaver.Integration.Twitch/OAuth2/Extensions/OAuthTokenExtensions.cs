using TwistedTaleweaver.Integration.Twitch.OAuth2.Model;
using HelixOAuthToken = TwistedTaleweaver.Integration.Twitch.Helix.Client.OAuthToken;

namespace TwistedTaleweaver.Integration.Twitch.OAuth2.Extensions;

public static class OAuthTokenExtensions
{
    public static HelixOAuthToken ToHelixOAuthToken(this Oauth2TokenResponse source)
    {
        return new HelixOAuthToken(source.AccessToken!);
    }
}