using System.Text.Json.Serialization;
using TwistedTaleweaver.Bridge.Twitch.Common.Models.Enums;

namespace TwistedTaleweaver.Bridge.Twitch.Common.Models;

/// <summary>
/// Represents the session information for a Twitch EventSub WebSocket connection.
/// </summary>
internal sealed class TwitchEventSubSession
{
    /// <summary>
    /// Unique identifier of the session.
    /// </summary>
    [JsonPropertyName("id")]
    public required string SessionId { get; set; }

    /// <summary>
    /// Current status of the session.
    /// </summary>
    [JsonPropertyName("status")]
    public SessionStatus Status { get; set; }

    /// <summary>
    /// Number of seconds after which the session will timeout without a keepalive message.
    /// </summary>
    [JsonPropertyName("keepalive_timeout_seconds")]
    public int? KeepaliveTimeoutSeconds { get; set; }
    
    /// <summary>
    /// URL to reconnect to the session if it is disconnected.
    /// </summary>
    [JsonPropertyName("reconnect_url")]
    public string? ReconnectUrl { get; set; }
}
