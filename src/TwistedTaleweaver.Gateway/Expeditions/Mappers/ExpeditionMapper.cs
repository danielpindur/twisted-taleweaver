using TwistedTaleweaver.Expeditions.Entities;
using ExpeditionContract = TwistedTaleweaver.Gateway.Expeditions.Contracts.Expedition;

namespace TwistedTaleweaver.Gateway.Expeditions.Mappers;

internal static class ExpeditionMapper
{
    public static ExpeditionContract ToContract(this Expedition source)
    {
        return new ExpeditionContract()
        {
            ExpeditionId =  source.ExpeditionId,
            Name =  source.Name,
            Description =  source.Description,
            Status =  source.Status,
            Difficulty =  source.Difficulty,
            MaxParticipants = source.MaxParticipants,
            Outcome = source.Outcome,
            CreatedAt = source.CreatedAt,
            UpdatedAt = source.UpdatedAt,
            StartedAt = source.StartedAt,
            CompletedAt = source.CompletedAt
        };
    }
}