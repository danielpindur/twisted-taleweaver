namespace TwistedTaleweaver.Bridge.Twitch.Configuration;

public class TwitchWebsocketConfiguration
{
    public required string EventSubWebSocketUrl { get; set; }
    
    public required List<BroadcasterConfiguration> Broadcasters { get; set; }
}
