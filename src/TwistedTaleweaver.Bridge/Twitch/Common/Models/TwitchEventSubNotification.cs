using System.Text.Json.Serialization;

namespace TwistedTaleweaver.Bridge.Twitch.Common.Models;

/// <summary>
/// Represents a notification received from the Twitch EventSub WebSocket connection.
/// </summary>
internal sealed class TwitchEventSubNotification
{
    /// <summary>
    /// Metadata about the notification.
    /// </summary>
    [JsonPropertyName("metadata")]
    public required TwitchEventSubMetadata Metadata { get; set; }

    /// <summary>
    /// Payload containing the event data.
    /// </summary>
    [JsonPropertyName("payload")]
    public required TwitchEventSubPayload Payload { get; set; }
}