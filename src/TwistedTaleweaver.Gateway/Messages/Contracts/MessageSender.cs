using TwistedTaleweaver.Api.Messages.Entities.Enums;

namespace TwistedTaleweaver.Gateway.Messages.Contracts;

[GraphQLDescription("A sender of a action message.")]
public class MessageSender
{
    [GraphQLDescription("Unique identifier of the creature.")]
    public Guid CreatureId { get; set; }

    [GraphQLDescription("Type of the action message sender.")]
    public SenderType SenderType { get; set; }
}