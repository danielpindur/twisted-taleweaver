using TwistedTaleweaver.Bridge.Twitch.Common.Processing;
using TwistedTaleweaver.Kafka.Api.Twitch.Payloads.Stream;

namespace TwistedTaleweaver.Bridge.Twitch.Events.StreamOnline.Mappers;

internal static class StreamOnlineMapper
{
    public static StreamStartedPayload ToStreamStartedPayload(this TwitchNotification notification)
    {
        var streamOnlineEvent = notification.Deserialize<StreamOnlineEvent>();
        
        return new StreamStartedPayload()
        {
            NotificationMessageId = notification.MessageId,
            NotificationMessageTimestamp = notification.MessageTimestamp,
            BroadcasterUserId = streamOnlineEvent.BroadcasterUserId,
            StreamId = streamOnlineEvent.StreamId,
            StartedAt = streamOnlineEvent.StartedAt
        };
    }
}