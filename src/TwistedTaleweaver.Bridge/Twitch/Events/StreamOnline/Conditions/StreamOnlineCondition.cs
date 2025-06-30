using System.Text.Json.Serialization;
using TwistedTaleweaver.Bridge.Twitch.Common.Conditions;

namespace TwistedTaleweaver.Bridge.Twitch.Events.StreamOnline.Conditions;

internal class StreamOnlineCondition(string broadcasterUserId) : ISubscriptionCondition
{
    /// <summary>
    /// The broadcaster user ID you want to get stream online notifications for.
    /// </summary>
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = broadcasterUserId;
}