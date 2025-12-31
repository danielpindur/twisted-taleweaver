using TwistedTaleweaver.Api.Messages.Entities.Enums;

namespace TwistedTaleweaver.Gateway.Messages.Contracts;

[GraphQLDescription("Represents a single chat message within the expedition.")]
public class ChatMessage
{
    [GraphQLDescription("Unique identifier of the message.")]
    public long Id { get; set; }

    [GraphQLDescription("The sender of the message.")]
    public required MessageSender? Sender { get; set; }

    [GraphQLDescription("The type of the message.")]
    public MessageType Type { get; set; }

    [GraphQLDescription("The string content of the message.")]
    public required string Content { get; set; }

    [GraphQLDescription("Timestamp of when the message was created within the system.")]
    public DateTimeOffset Timestamp { get; set; }
}