using System.Runtime.Serialization;

namespace TwistedTaleweaver.Bridge.Twitch.Common.Models.Enums;

/// <summary>
/// Represents the type of subscription for Twitch EventSub notifications.
/// </summary>
internal enum SubscriptionType
{
    /// <summary>
    /// Subscription for when a stream goes online.
    /// </summary>
    [EnumMember(Value = "stream.online")]
    StreamOnline = 1,
    
    /// <summary>
    /// Subscription for when a stream goes offline.
    /// </summary>
    
    [EnumMember(Value = "stream.offline")]
    StreamOffline = 2,
    
    /// <summary>
    /// Subscription for chat messages in a channel.
    /// </summary>
    [EnumMember(Value = "channel.chat.message")]
    ChannelChatMessage = 3,
}