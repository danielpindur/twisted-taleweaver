using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;
using TwistedTaleweaver.Domain.Expeditions;

namespace TwistedTaleweaver.Expeditions.QueryHandlers;

internal class ExpeditionStatesQueryHandler(IExpeditionsReader expeditionsReader) : IExpeditionStatesQueryHandler
{
    public Task<IEnumerable<ExpeditionState>> ExecuteAsync(ExpeditionStatesQuery query)
    {
        return expeditionsReader.GetStatesAsync(query.ExpeditionIds);
    }
}