using TwistedTaleweaver.Bridge.Twitch.Common.Processing;
using TwistedTaleweaver.Bridge.Twitch.Events.StreamOnline.Mappers;
using TwistedTaleweaver.Core.Kafka.Extensions;
using TwistedTaleweaver.Core.Kafka.Producers;
using TwistedTaleweaver.Kafka.Api.Twitch;

namespace TwistedTaleweaver.Bridge.Twitch.Events.StreamOnline.Processing;

internal class StreamOnlineNotificationProcessor(IKafkaEventProducer kafkaEventProducer) : INotificationProcessor
{
    public async Task ProcessAsync(TwitchNotification notification)
    {
        await kafkaEventProducer.PublishAsync(
            TwitchEvent.StreamStarted.WithPayload(notification.ToStreamStartedPayload()));
    }
}