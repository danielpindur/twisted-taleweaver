using TwistedTaleweaver.Api.Expeditions.Entities;
using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;

namespace TwistedTaleweaver.Expeditions.QueryHandlers;

internal class ExpeditionByIdQueryHandler(IExpeditionsReader expeditionsReader) : IExpeditionByIdQueryHandler
{
    public Task<Expedition?> ExecuteAsync(ExpeditionByIdQuery query)
    {
        return expeditionsReader.GetByIdAsync(query.ExpeditionId);
    }
}