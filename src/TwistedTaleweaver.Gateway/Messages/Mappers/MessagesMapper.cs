using TwistedTaleweaver.Api.Messages.Entities;
using TwistedTaleweaver.Api.Messages.Events;
using TwistedTaleweaver.Api.Messages.Queries;
using TwistedTaleweaver.Gateway.Common.Pagination.Contracts;
using ChatMessageContract = TwistedTaleweaver.Gateway.Messages.Contracts.ChatMessage;
using MessageSenderContract = TwistedTaleweaver.Gateway.Messages.Contracts.MessageSender;

namespace TwistedTaleweaver.Gateway.Messages.Mappers;

internal static class MessagesMapper
{
    public static CursorPage<ChatMessageContract, long> ToContract(this PaginatedMessagesSearchResult source)
    {
        return new CursorPage<ChatMessageContract, long>()
        {
            Items = source.Messages.ToContracts(),
            EndCursor = source.Messages.FirstOrDefault()?.MessageId,
            HasNextPage = source.HasNextPage
        };
    }

    public static List<ChatMessageContract> ToContracts(this List<ChatMessage> source)
    {
        return source.Select(ToContract).ToList();
    }
    
    public static ChatMessageContract ToContract(this ChatMessage source)
    {
        return new ChatMessageContract()
        {
            Id = source.MessageId,
            Sender = source.Sender.ToContract(),
            Type = source.Type,
            Content = source.Content,
            Timestamp = source.Timestamp
        };
    }

    public static MessageSenderContract? ToContract(this MessageSender? source)
    {
        if (source is null)
        {
            return null;
        }

        return new MessageSenderContract()
        {
            CreatureId = source.CreatureId,
            SenderType = source.SenderType,
        };
    }

    public static ChatMessageContract ToContract(this MessageCreatedEvent source)
    {
        return new ChatMessageContract()
        {
            Id = source.MessageId,
            Sender = source.Sender.ToContract(),
            Type = source.Type,
            Content = source.Content,
            Timestamp = source.Timestamp
        };
    }
}