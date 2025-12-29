using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;
using TwistedTaleweaver.Domain.Expeditions;
using TwistedTaleweaver.Expeditions.Mappers;

namespace TwistedTaleweaver.Expeditions.QueryHandlers;

internal class ExpeditionByIdQueryHandler(
    IExpeditionsReader expeditionsReader,
    IExpeditionParticipantsQueryHandler expeditionParticipantsQueryHandler) : IExpeditionByIdQueryHandler
{
    public async Task<Expedition?> ExecuteAsync(ExpeditionByIdQuery query)
    {
        var dto = await expeditionsReader.GetByIdAsync(query.ExpeditionId);

        if (dto is null)
        {
            return null;
        }

        var participants = await expeditionParticipantsQueryHandler.ExecuteAsync(
            new ExpeditionParticipantsQuery() { ExpeditionIds = [query.ExpeditionId] });
        
        return dto.ToExpedition(participants[query.ExpeditionId]);
    }
}