using HotChocolate.Subscriptions;
using TwistedTaleweaver.Api.Messages.Events;
using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.Gateway.Messages.Helpers;

namespace TwistedTaleweaver.Gateway.Messages.Events;

internal class MessageCreatedEventConsumer(ITopicEventSender topicEventSender) : IKafkaEventConsumer<MessageCreatedEvent>
{
    public async Task ConsumeAsync(MessageCreatedEvent @event, CancellationToken cancellationToken)
    {
        await topicEventSender.PublishMessageCreatedAsync(@event);
    }
}