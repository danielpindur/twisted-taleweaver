using TwistedTaleweaver.Bridge.Twitch.Common.Processing;
using TwistedTaleweaver.Bridge.Twitch.Events.StreamOffline.Mappers;
using TwistedTaleweaver.Core.Kafka.Extensions;
using TwistedTaleweaver.Core.Kafka.Producers;
using TwistedTaleweaver.Kafka.Api.Twitch;

namespace TwistedTaleweaver.Bridge.Twitch.Events.StreamOffline.Processing;

internal class StreamOfflineNotificationProcessor(IKafkaEventProducer kafkaEventProducer) : INotificationProcessor
{
    public async Task ProcessAsync(TwitchNotification notification)
    {
        await kafkaEventProducer.PublishAsync(
            TwitchEvent.StreamEnded.WithPayload(notification.ToStreamEndedPayload()));
    }
}