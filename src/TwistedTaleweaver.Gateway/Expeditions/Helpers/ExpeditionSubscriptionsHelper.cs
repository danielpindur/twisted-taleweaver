using HotChocolate.Subscriptions;
using TwistedTaleweaver.Gateway.Expeditions.Contracts;

namespace TwistedTaleweaver.Gateway.Expeditions.Helpers;

internal static class ExpeditionSubscriptionsHelper
{
    public static ValueTask PublishParticipantsChangedAsync(
        this ITopicEventSender topicEventSender, 
        Expedition expedition)
    {
        var topicName = $"{nameof(ExpeditionsSubscriptions.ParticipantsUpdated)}_{expedition.Id}";
        return topicEventSender.SendAsync(topicName, expedition);
    }
    
    public static ValueTask PublishExpeditionStateChangedAsync(
        this ITopicEventSender topicEventSender, 
        ExpeditionState expeditionState)
    {
        var topicName = $"{nameof(ExpeditionsSubscriptions.ExpeditionStateChanged)}_{expeditionState.ExpeditionId}";
        return topicEventSender.SendAsync(topicName, expeditionState);
    }
}