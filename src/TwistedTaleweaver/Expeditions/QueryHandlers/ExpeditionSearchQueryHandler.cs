using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.Core.Pagination;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;
using TwistedTaleweaver.Domain.Expeditions;

namespace TwistedTaleweaver.Expeditions.QueryHandlers;

internal class ExpeditionSearchQueryHandler(
    IExpeditionsReader expeditionsReader,
    IExpeditionParticipantsQueryHandler expeditionParticipantsQueryHandler) : IExpeditionSearchQueryHandler
{
    public async Task<PaginatedResult<ExpeditionSlim>> ExecuteAsync(ExpeditionSearchQuery query)
    {
        var dtos = (await expeditionsReader.GetPageAsync(query.UserId, query.PageNumber, query.PageSize)).ToList();
        
        var participantsByExpeditionId = await expeditionParticipantsQueryHandler.ExecuteAsync(
            new ExpeditionsParticipantsQuery() { ExpeditionIds = dtos.Select(ep => ep.ExpeditionId).ToList() });

        foreach (var dto in dtos)
        {
            dto.CurrentParticipants = participantsByExpeditionId.TryGetValue(dto.ExpeditionId, out var expeditionParticipants) 
                ? (byte)expeditionParticipants.Count
                : byte.MinValue;
        }

        return new PaginatedResult<ExpeditionSlim>(dtos, dtos.FirstOrDefault()?.TotalCount ?? 0, query);
    }
}