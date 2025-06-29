using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwistedTaleweaver.Bridge.Twitch.Common.Models;

/// <summary>
/// Represents the payload of a Twitch EventSub notification.
/// </summary>
internal sealed class TwitchEventSubPayload
{
    /// <summary>
    /// Session information for WebSocket connections.
    /// </summary>
    [JsonPropertyName("session")]
    public TwitchEventSubSession? Session { get; set; }

    /// <summary>
    /// Subscription information for the event.
    /// </summary>
    [JsonPropertyName("subscription")]
    public TwitchEventSubSubscription? Subscription { get; set; }

    /// <summary>
    /// Actual event data.
    /// </summary>
    [JsonPropertyName("event")]
    public JsonElement? Event { get; set; }
}
