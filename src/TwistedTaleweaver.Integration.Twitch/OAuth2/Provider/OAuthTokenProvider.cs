using TwistedTaleweaver.Integration.Twitch.Common.Configuration;
using TwistedTaleweaver.Integration.Twitch.Helix.Client;
using TwistedTaleweaver.Integration.Twitch.OAuth2.Api;
using TwistedTaleweaver.Integration.Twitch.OAuth2.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TwistedTaleweaver.Integration.Twitch.OAuth2.Provider;

public class OAuthTokenProvider : Helix.TokenProvider<OAuthToken>
{
    private const string RefreshTokenGrantType = "refresh_token";
    
    private readonly string _clientId;
    private readonly string _clientSecret;
    
    private readonly IOAuthApi _oauthApi;
    private readonly ILogger<OAuthTokenProvider> _logger;
    
    private readonly SemaphoreSlim _refreshLock = new(1, 1);
    private DateTime? _expiresAtUtc;
    private string? _refreshToken;

    /// <summary>
    /// Creates a new instance of the OAuthTokenProvider.
    /// </summary>
    /// <param name="oauthApi">An instance of the IOAuthApi to interact with Twitch's OAuth API.</param>
    /// <param name="logger">Logger for logging purposes.</param>
    /// <param name="configuration">Configuration containing the Client ID and Client Secret.</param>
    public OAuthTokenProvider(
        IOAuthApi oauthApi,
        ILogger<OAuthTokenProvider> logger,
        IOptions<TwitchApiConfiguration> configuration)
    {
        _oauthApi = oauthApi;
        _logger = logger;
        
        _clientId = configuration.Value.ClientId;
        _clientSecret = configuration.Value.ClientSecret;
        _refreshToken = configuration.Value.RefreshToken;
    }

    /// <summary>
    /// Returns a valid OAuthToken, refreshing it if it is close to expiring.
    /// </summary>
    /// <param name="header">Optional header information</param>
    /// <param name="cancellation">A cancellation token.</param>
    /// <returns>An up‑to‑date OAuthToken.</returns>
    internal override async ValueTask<OAuthToken> GetAsync(
        string header = "",
        CancellationToken cancellation = default)
    {
        if (HasInvalidToken())
        {
            await ResolveTokenAsync(cancellation);
        }

        return GetValidToken();
    }

    private async Task ResolveTokenAsync(CancellationToken cancellation)
    {
        // Ensure that only one refresh occurs at a time.
        await _refreshLock.WaitAsync(cancellation);

        try
        {
            // Re-check after acquiring the lock as other process might have already refreshed it.
            if (!HasInvalidToken())
            {
                return;
            }

            await RefreshToken(cancellation);
        }
        finally
        {
            _refreshLock.Release();
        }
    }
    
    private async Task RefreshToken(CancellationToken cancellation)
    {
        var authenticationResponse = await _oauthApi.Oauth2TokenPostAsync(
            _clientId, 
            _clientSecret,
            RefreshTokenGrantType,
            refreshToken: _refreshToken!,
            cancellationToken: cancellation);
        
        if (!authenticationResponse.TryOk(out var oauth2Token))
        {
            _logger.LogCritical(
                "Failed to refresh OAuth token: {StatusCode} {ReasonPhrase}",
                authenticationResponse.StatusCode,
                authenticationResponse.ReasonPhrase);
            
            throw new ApiException("Failed to refresh OAuth token", authenticationResponse.StatusCode, authenticationResponse.RawContent);
        }
        
        _expiresAtUtc = DateTime.UtcNow.AddSeconds(oauth2Token.ExpiresIn!.Value);
        _refreshToken = oauth2Token.RefreshToken;
        _tokens[0] = oauth2Token.ToHelixOAuthToken();
        
        _logger.LogInformation("OAuth token refreshed successfully. Expires at: {ExpiresAt}", _expiresAtUtc.Value.ToLocalTime());
    }

    private bool HasInvalidToken()
    {
        return _tokens.FirstOrDefault() is null ||
               _expiresAtUtc is null ||
               _expiresAtUtc.Value.AddMinutes(-1) < DateTime.UtcNow;
    }

    private OAuthToken GetValidToken()
    {
        return _tokens.Single();
    }
}