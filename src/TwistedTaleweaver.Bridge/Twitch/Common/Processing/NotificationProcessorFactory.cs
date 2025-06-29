using TwistedTaleweaver.Bridge.Twitch.Common.Models.Enums;
using TwistedTaleweaver.Bridge.Twitch.Events.ChannelChatMessage.Processing;
using TwistedTaleweaver.Bridge.Twitch.Events.StreamOffline.Processing;
using TwistedTaleweaver.Bridge.Twitch.Events.StreamOnline.Processing;

namespace TwistedTaleweaver.Bridge.Twitch.Common.Processing;

internal interface INotificationProcessorFactory
{
    INotificationProcessor Create(SubscriptionType subscriptionType);
}

internal class NotificationProcessorFactory(IServiceProvider provider) : INotificationProcessorFactory
{
    public INotificationProcessor Create(SubscriptionType subscriptionType)
    {
        return subscriptionType switch
        {
            SubscriptionType.StreamOnline => provider.GetRequiredService<StreamOnlineNotificationProcessor>(),
            SubscriptionType.StreamOffline => provider.GetRequiredService<StreamOfflineNotificationProcessor>(),
            SubscriptionType.ChannelChatMessage => provider.GetRequiredService<ChannelChatMessageNotificationProcessor>(),
            
            _ => throw new ArgumentOutOfRangeException(nameof(subscriptionType), subscriptionType, $"Unsupported subscription type: {subscriptionType}")
        };
    }
}