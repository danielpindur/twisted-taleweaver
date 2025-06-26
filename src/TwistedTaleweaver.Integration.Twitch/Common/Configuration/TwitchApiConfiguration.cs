namespace TwistedTaleweaver.Integration.Twitch.Common.Configuration;

public class TwitchApiConfiguration
{
    public required string HelixApiUrl {get; set; } 
    
    public required string OAuthApiUrl {get; set; } 
    
    public required string ClientId { get; set; }
    
    public required string ClientSecret { get; set; }
    
    public required string RefreshToken { get; set; } 
}