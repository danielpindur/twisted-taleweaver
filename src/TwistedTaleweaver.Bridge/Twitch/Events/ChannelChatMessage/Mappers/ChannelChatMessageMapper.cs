using TwistedTaleweaver.Bridge.Twitch.Common.Processing;
using TwistedTaleweaver.Kafka.Api.Twitch.Payloads.Chat;

namespace TwistedTaleweaver.Bridge.Twitch.Events.ChannelChatMessage.Mappers;

internal static class ChannelChatMessageMapper
{
    public static ChatMessagePayload ToChatMessagePayload(this TwitchNotification notification)
    {
        var channelChatMessageEvent = notification.Deserialize<ChannelChatMessageEvent>();
        
        return new ChatMessagePayload()
        {
            NotificationMessageId = notification.MessageId,
            NotificationMessageTimestamp = notification.MessageTimestamp,
            BroadcasterUserId = channelChatMessageEvent.BroadcasterUserId,
            MessageId = channelChatMessageEvent.ChatMessageId,
            ChatterUserId = channelChatMessageEvent.ChatterUserId,
            ChatterUserName = channelChatMessageEvent.ChatterUserName,
            Message = channelChatMessageEvent.Message.Text
        };
    }
}