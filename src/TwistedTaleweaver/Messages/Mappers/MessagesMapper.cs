using TwistedTaleweaver.Api.Messages.Entities;
using TwistedTaleweaver.Api.Messages.Entities.Enums;
using TwistedTaleweaver.Api.Messages.Events;
using TwistedTaleweaver.DataAccess.Messages.Dtos;

namespace TwistedTaleweaver.Messages.Mappers;

internal static class MessagesMapper
{
    public static List<ChatMessage> ToChatMessages(this IEnumerable<MessageDto> source)
    {
        return source.Select(ToChatMessage).ToList();
    }

    public static ChatMessage ToChatMessage(this MessageDto source)
    {
        return new ChatMessage()
        {
            MessageId = source.MessageId,
            Type = source.Type,
            Timestamp = source.CreatedAt,
            Sender = source.ToMessageSender(),
            Content = source.Content
        };
    }

    public static MessageCreatedEvent ToMessageCreatedEvent(this MessageDto source)
    {
        return new MessageCreatedEvent()
        {
            MessageId = source.MessageId,
            ExpeditionId = source.ExpeditionId,
            Type = source.Type,
            Timestamp = source.CreatedAt,
            Sender = source.ToMessageSender(),
            Content = source.Content
        };
    }

    public static MessageSender? ToMessageSender(this MessageDto source)
    {
        if (source.SenderCharacterId.HasValue)
        {
            return new MessageSender()
            {
                CreatureId = source.SenderCharacterId.Value,
                SenderType = SenderType.Character
            };
        }

        return null;
    }
}