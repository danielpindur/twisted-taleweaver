using TwistedTaleweaver.Bridge.Twitch.Common.Processing;
using TwistedTaleweaver.Kafka.Api.Twitch.Payloads.Chat;

namespace TwistedTaleweaver.Bridge.Twitch.Events.ChannelChatMessage.Mappers;

internal static class ChannelChatMessageMapper
{
    public static ChatMessagePayload ToChatMessagePayload(this TwitchNotification notification)
    {
        var streamOnlineEvent = notification.Deserialize<ChannelChatMessageEvent>();
        
        return new ChatMessagePayload()
        {
            NotificationMessageId = notification.MessageId,
            NotificationMessageTimestamp = notification.MessageTimestamp,
            BroadcasterUserId = streamOnlineEvent.BroadcasterUserId,
            MessageId = streamOnlineEvent.ChatMessageId,
            ChatterUserId = streamOnlineEvent.ChatterUserId,
            ChatterUserName = streamOnlineEvent.ChatterUserName,
            Message = streamOnlineEvent.Message.Text
        };
    }
}