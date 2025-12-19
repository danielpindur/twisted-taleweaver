using TwistedTaleweaver.DataAccess.Messages.Dtos;
using TwistedTaleweaver.Domain.Messages;
using TwistedTaleweaver.Domain.Messages.Enums;

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
            ExpeditionMessageId = source.ExpeditionMessageId,
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