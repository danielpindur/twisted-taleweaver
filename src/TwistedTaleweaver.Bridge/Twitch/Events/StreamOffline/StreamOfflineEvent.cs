using System.Text.Json.Serialization;
using TwistedTaleweaver.Bridge.Twitch.Common.Models;

namespace TwistedTaleweaver.Bridge.Twitch.Events.StreamOffline;

/// <summary>
/// Represents a Twitch stream going offline event.
/// </summary>
internal sealed class StreamOfflineEvent : ITwitchEvent
{
    /// <summary>
    /// Unique identifier of the broadcaster who went offline.
    /// </summary>
    [JsonPropertyName("broadcaster_user_id")]
    public required string BroadcasterUserId { get; set; }

    /// <summary>
    /// Login name of the broadcaster who went offline.
    /// </summary>
    [JsonPropertyName("broadcaster_user_login")]
    public required string BroadcasterUserLogin { get; set; }

    /// <summary>
    /// Display name of the broadcaster who went offline.
    /// </summary>
    [JsonPropertyName("broadcaster_user_name")]
    public required string BroadcasterUserName { get; set; }
}