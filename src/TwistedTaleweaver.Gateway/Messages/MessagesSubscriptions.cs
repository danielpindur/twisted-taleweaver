using HotChocolate.Authorization;
using TwistedTaleweaver.Gateway.Messages.Contracts;

namespace TwistedTaleweaver.Gateway.Messages;

[SubscriptionType]
public static class MessagesSubscriptions
{
    [GraphQLDescription("Notifies when a new message has been created in an expedition.")]
    [Subscribe]
    [Authorize]
    [Topic($"{nameof(MessageCreated)}_{{{nameof(expeditionId)}}}")]
    public static ChatMessage MessageCreated(Guid expeditionId, [EventMessage] ChatMessage message)
    {
        return message;
    }
}