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
        var expeditionTask = expeditionsReader.GetByIdAsync(query.ExpeditionId);
        var participantsTask = expeditionParticipantsReader.GetByExpeditionAsync(query.ExpeditionId);

        var expedition = await expeditionTask;

        if (expedition is null)
        {
            return null;
        }

        expedition.CurrentParticipants = (await participantsTask).Count();
        
        return expedition;
    }
}