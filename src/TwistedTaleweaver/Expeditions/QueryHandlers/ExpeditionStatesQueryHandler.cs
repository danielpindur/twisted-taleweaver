using TwistedTaleweaver.Api.Expeditions.Entities;
using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;

namespace TwistedTaleweaver.Expeditions.QueryHandlers;

internal class ExpeditionStatesQueryHandler(IExpeditionsReader expeditionsReader) : IExpeditionStatesQueryHandler
{
    public Task<IEnumerable<ExpeditionState>> ExecuteAsync(ExpeditionStatesQuery query)
    {
        return expeditionsReader.GetStatesAsync(query.ExpeditionIds);
    }

    public async Task<ExpeditionState> ExecuteAsync(ExpeditionStateQuery query)
    {
        var expeditionStateById = await ExecuteAsync(
            new ExpeditionStatesQuery() { ExpeditionIds = [query.ExpeditionId] });

        return expeditionStateById.Single();
    }
}