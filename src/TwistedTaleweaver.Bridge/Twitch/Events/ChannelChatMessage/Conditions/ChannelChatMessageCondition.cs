using System.Text.Json.Serialization;
using TwistedTaleweaver.Bridge.Twitch.Common.Conditions;

namespace TwistedTaleweaver.Bridge.Twitch.Events.ChannelChatMessage.Conditions;

internal class ChannelChatMessageCondition(string broadcasterUserId, string userId) : ISubscriptionCondition
{
    /// <summary>
    /// The User ID of the channel to receive chat message events for.
    /// </summary>
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = broadcasterUserId;

    /// <summary>
    /// The User ID to read chat as.
    /// </summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; set; } = userId;
}