using HotChocolate.Authorization;
using TwistedTaleweaver.Gateway.Expeditions.Contracts;

namespace TwistedTaleweaver.Gateway.Expeditions;

[SubscriptionType]
public static class ExpeditionsSubscriptions
{
    [GraphQLDescription("Notifies when the participants of an expedition have been updated.")]
    [Subscribe]
    [Authorize]
    [Topic($"{nameof(ParticipantsUpdated)}_{{{nameof(expeditionId)}}}")]
    public static Expedition ParticipantsUpdated(Guid expeditionId, [EventMessage] Expedition expedition)
    {
        return expedition;
    }
    
    [GraphQLDescription("Notifies when the state of an expedition has changed.")]
    [Subscribe]
    [Authorize]
    [Topic($"{nameof(ExpeditionStateChanged)}_{{{nameof(expeditionId)}}}")]
    public static Expedition ExpeditionStateChanged(Guid expeditionId, [EventMessage] Expedition expedition)
    {
        return expedition;
    }
}