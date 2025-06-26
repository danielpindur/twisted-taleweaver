using TwistedTaleweaver.Bridge.Twitch.Common.Processing;
using TwistedTaleweaver.Bridge.Twitch.Events.ChannelChatMessage.Mappers;
using TwistedTaleweaver.Core.Kafka.Extensions;
using TwistedTaleweaver.Core.Kafka.Producers;
using TwistedTaleweaver.Kafka.Api.Twitch;

namespace TwistedTaleweaver.Bridge.Twitch.Events.ChannelChatMessage.Processing;

internal class ChannelChatMessageNotificationProcessor(IKafkaEventProducer kafkaEventProducer) : INotificationProcessor
{
    public async Task ProcessAsync(TwitchNotification notification)
    {
        await kafkaEventProducer.PublishAsync(
            TwitchEvent.ChatMessage.WithPayload(notification.ToChatMessagePayload()));
    }
}