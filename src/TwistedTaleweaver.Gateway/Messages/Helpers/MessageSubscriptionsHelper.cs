using HotChocolate.Subscriptions;
using TwistedTaleweaver.Api.Messages.Events;
using TwistedTaleweaver.Gateway.Messages.Mappers;

namespace TwistedTaleweaver.Gateway.Messages.Helpers;

internal static class MessageSubscriptionsHelper
{
    public static ValueTask PublishMessageCreatedAsync(
        this ITopicEventSender topicEventSender, 
        MessageCreatedEvent @event)
    {
        var topicName = $"{nameof(MessagesSubscriptions.MessageCreated)}_{@event.ExpeditionId}";
        return topicEventSender.SendAsync(topicName, @event.ToContract());
    }
}