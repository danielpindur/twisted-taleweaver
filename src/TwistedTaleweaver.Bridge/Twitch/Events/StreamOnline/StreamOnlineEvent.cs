using System.Text.Json.Serialization;
using TwistedTaleweaver.Bridge.Twitch.Common.Models;
using TwistedTaleweaver.Bridge.Twitch.Events.StreamOnline.Enums;

namespace TwistedTaleweaver.Bridge.Twitch.Events.StreamOnline;

/// <summary>
/// Represents a Twitch stream going online event.
/// </summary>
internal sealed class StreamOnlineEvent : ITwitchEvent
{
    /// <summary>
    /// Unique identifier of the broadcaster who went live.
    /// </summary>
    [JsonPropertyName("broadcaster_user_id")]
    public required string BroadcasterUserId { get; set; }

    /// <summary>
    /// Login name of the broadcaster who went live.
    /// </summary>
    [JsonPropertyName("broadcaster_user_login")]
    public required string BroadcasterUserLogin { get; set; }

    /// <summary>
    /// Display name of the broadcaster who went live.
    /// </summary>
    [JsonPropertyName("broadcaster_user_name")]
    public required string BroadcasterUserName { get; set; }

    /// <summary>
    /// Unique identifier of the stream.
    /// </summary>
    [JsonPropertyName("id")]
    public required string StreamId { get; set; }

    /// <summary>
    /// Type of the stream.
    /// </summary>
    [JsonPropertyName("type")]
    public StreamType StreamType { get; set; }

    /// <summary>
    /// DateTime when the stream started.
    /// </summary>
    [JsonPropertyName("started_at")]
    public DateTime StartedAt { get; set; }
}
