using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace TwistedTaleweaver.Integration.Twitch.Common.Configuration;

/// <summary>
/// Configuration settings for Twitch API integration
/// </summary>
public class TwitchApiConfiguration
{
    /// <summary>
    /// The base URL for Twitch Helix API
    /// </summary>
    [Required, Url]
    public required string HelixApiUrl {get; set; } 
    
    /// <summary>
    /// The base URL for Twitch OAuth API
    /// </summary>
    [Required, Url]
    public required string OAuthApiUrl {get; set; } 
    
    /// <summary>
    /// The Twitch application client ID
    /// </summary>
    [Required]
    public required string ClientId { get; set; }
    
    /// <summary>
    /// The Twitch application client secret
    /// </summary>
    [Required, DebuggerHidden]
    public required string ClientSecret { get; set; }
    
    /// <summary>
    /// The OAuth refresh token for API authentication
    /// </summary>
    [Required, DebuggerHidden]
    public required string RefreshToken { get; set; } 
}