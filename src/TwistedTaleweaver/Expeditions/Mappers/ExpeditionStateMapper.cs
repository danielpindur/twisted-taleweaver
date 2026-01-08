using TwistedTaleweaver.Api.Expeditions.Entities;
using TwistedTaleweaver.Api.Expeditions.Events;

namespace TwistedTaleweaver.Expeditions.Mappers;

internal static class ExpeditionStateMapper
{
    public static ExpeditionStateUpdatedEvent ToExpeditionStateUpdatedEvent(this ExpeditionState source)
    {
        return new ExpeditionStateUpdatedEvent()
        {
            ExpeditionId = source.ExpeditionId,
            Status = source.Status,
            ActiveCreatureId = source.ActiveCreatureId,
            Monsters = source.Monsters
        };
    }
}