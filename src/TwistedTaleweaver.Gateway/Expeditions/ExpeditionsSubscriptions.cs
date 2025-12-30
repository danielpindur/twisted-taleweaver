using TwistedTaleweaver.Gateway.Expeditions.Contracts;

namespace TwistedTaleweaver.Gateway.Expeditions;

[SubscriptionType]
public static class ExpeditionsSubscriptions
{
    [Subscribe]
    [Topic($"ParticipantsUpdated_{{{nameof(expeditionId)}}}")]
    public static Expedition ParticipantsUpdated(Guid expeditionId, [EventMessage] Expedition expedition)
    {
        return expedition;
    }
}