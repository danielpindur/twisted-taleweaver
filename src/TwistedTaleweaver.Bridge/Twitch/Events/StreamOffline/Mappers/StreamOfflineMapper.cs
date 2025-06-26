using TwistedTaleweaver.Bridge.Twitch.Common.Processing;
using TwistedTaleweaver.Kafka.Api.Twitch.Payloads.Stream;

namespace TwistedTaleweaver.Bridge.Twitch.Events.StreamOffline.Mappers;

internal static class StreamOfflineMapper
{
    public static StreamEndedPayload ToStreamEndedPayload(this TwitchNotification notification)
    {
        var streamOnlineEvent = notification.Deserialize<StreamOfflineEvent>();
        
        return new StreamEndedPayload()
        {
            NotificationMessageId = notification.MessageId,
            NotificationMessageTimestamp = notification.MessageTimestamp,
            BroadcasterUserId = streamOnlineEvent.BroadcasterUserId
        };
    }
}