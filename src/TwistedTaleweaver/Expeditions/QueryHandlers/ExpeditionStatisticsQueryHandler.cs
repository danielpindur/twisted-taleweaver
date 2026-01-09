using TwistedTaleweaver.Api.Expeditions.Entities;
using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;

namespace TwistedTaleweaver.Expeditions.QueryHandlers;

internal class ExpeditionStatisticsQueryHandler(IExpeditionsReader expeditionsReader) : IExpeditionStatisticsQueryHandler
{
    public async Task<ExpeditionStatistics> ExecuteAsync(ExpeditionStatisticsQuery query)
    {
        var statistics = await expeditionsReader.GetStatisticsAsync(query);
        return statistics ?? new  ExpeditionStatistics();
    }
}