using HotChocolate.Subscriptions;
using TwistedTaleweaver.Api.Expeditions.Events;
using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.Gateway.Expeditions.Helpers;
using TwistedTaleweaver.Gateway.Expeditions.Mappers;

namespace TwistedTaleweaver.Gateway.Expeditions.Events;

internal class ExpeditionStateUpdatedEventConsumer(ITopicEventSender topicEventSender) : IKafkaEventConsumer<ExpeditionStateUpdatedEvent>
{
    public async Task ConsumeAsync(ExpeditionStateUpdatedEvent @event, CancellationToken cancellationToken)
    {
        await topicEventSender.PublishExpeditionStateChangedAsync(@event.ToContract());
    }
}