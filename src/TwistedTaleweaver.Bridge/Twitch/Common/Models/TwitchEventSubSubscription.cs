using System.Text.Json.Serialization;
using TwistedTaleweaver.Bridge.Twitch.Common.Models.Enums;

namespace TwistedTaleweaver.Bridge.Twitch.Common.Models;

/// <summary>
/// Represents the subscription information for a Twitch EventSub notification.
/// </summary>
internal sealed class TwitchEventSubSubscription
{
    /// <summary>
    /// Unique identifier of the subscription.
    /// </summary>
    [JsonPropertyName("id")]
    public required string SubscriptionId { get; set; }

    /// <summary>
    /// Current status of the subscription.
    /// </summary>
    [JsonPropertyName("status")]
    public SubscriptionStatus Status { get; set; }

    /// <summary>
    /// Type of the subscription (e.g., stream.online).
    /// </summary>
    /// <remarks>Nullable only for purpose of deserialization, so it doesn't fail when we don't have the enum value in our enum. Will always be non-null when used.</remarks>
    [JsonPropertyName("type")]
    public SubscriptionType? Type { get; set; }
}
