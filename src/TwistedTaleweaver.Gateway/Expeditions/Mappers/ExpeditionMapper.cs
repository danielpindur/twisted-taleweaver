using TwistedTaleweaver.Common.Pagination.Entities;
using TwistedTaleweaver.Expeditions.Entities;
using TwistedTaleweaver.Gateway.Common.Pagination.Contracts;
using TwistedTaleweaver.Gateway.Common.Pagination.Mappers;
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

    public static PaginatedResponse<ExpeditionContract> ToContract(this PaginatedResult<Expedition> source)
    {
        return source.ToContract(ToContract);
    }
}