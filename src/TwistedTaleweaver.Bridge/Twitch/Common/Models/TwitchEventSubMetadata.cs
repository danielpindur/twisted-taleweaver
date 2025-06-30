using System.Text.Json.Serialization;
using TwistedTaleweaver.Bridge.Twitch.Common.Models.Enums;

namespace TwistedTaleweaver.Bridge.Twitch.Common.Models;

/// <summary>
/// Represents metadata about a Twitch EventSub message.
/// </summary>
internal sealed class TwitchEventSubMetadata
{
    /// <summary>
    /// Unique identifier of the message.
    /// </summary>
    [JsonPropertyName("message_id")]
    public required string MessageId { get; set; }

    /// <summary>
    /// Type of the message (e.g., notification, welcome, keepalive).
    /// </summary>
    [JsonPropertyName("message_type")]
    public MessageType MessageType { get; set; }

    /// <summary>
    /// Timestamp when the message was sent.
    /// </summary>
    [JsonPropertyName("message_timestamp")]
    public DateTime MessageTimestamp { get; set; }

    /// <summary>
    /// Type of subscription this message relates to (e.g., stream.online).
    /// </summary>
    [JsonPropertyName("subscription_type")]
    public SubscriptionType? SubscriptionType { get; set; }

    /// <summary>
    /// The version of the subscription this message relates to.
    /// </summary>
    [JsonPropertyName("subscription_version")]
    public int? SubscriptionVersion { get; set; }
}
