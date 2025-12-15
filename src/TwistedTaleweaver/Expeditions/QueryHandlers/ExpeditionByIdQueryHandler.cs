using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;
using TwistedTaleweaver.Domain.Expeditions;

namespace TwistedTaleweaver.Expeditions.QueryHandlers;

internal class ExpeditionByIdQueryHandler(
    IExpeditionsReader expeditionsReader,
    IExpeditionParticipantsReader expeditionParticipantsReader) : IExpeditionByIdQueryHandler
{
    public async Task<Expedition?> ExecuteAsync(ExpeditionByIdQuery query)
    {
        var expedition = await expeditionsReader.GetByIdAsync(query.ExpeditionId);
        
        if (expedition is null)
        {
            return null;
        }
        
        var participants = await expeditionParticipantsReader.GetByExpeditionAsync(query.ExpeditionId);

        expedition.CurrentParticipants = participants.Count();
        
        return expedition;
    }
}