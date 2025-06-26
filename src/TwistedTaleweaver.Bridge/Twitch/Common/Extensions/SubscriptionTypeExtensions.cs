using TwistedTaleweaver.Bridge.Twitch.Common.Models.Enums;
using TypeEnum = TwistedTaleweaver.Integration.Twitch.Helix.Model.CreateEventSubSubscriptionBody.TypeEnum;

namespace TwistedTaleweaver.Bridge.Twitch.Common.Extensions;

internal static class SubscriptionTypeExtensions
{
    public static TypeEnum ToApiType(this SubscriptionType subscriptionType)
    {
        return subscriptionType switch
        {
            SubscriptionType.StreamOnline => TypeEnum.StreamOnline,
            SubscriptionType.StreamOffline => TypeEnum.StreamOffline,
            SubscriptionType.ChannelChatMessage => TypeEnum.ChannelChatMessage,
            
            _ => throw new ArgumentOutOfRangeException(nameof(subscriptionType), subscriptionType, null)
        };
    }
}