using TwistedTaleweaver.Api.Expeditions.Entities;
using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.Core.Pagination;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;

namespace TwistedTaleweaver.Expeditions.QueryHandlers;

internal class ExpeditionSearchQueryHandler(IExpeditionsReader expeditionsReader) : IExpeditionSearchQueryHandler
{
    public async Task<PaginatedResult<Expedition>> ExecuteAsync(ExpeditionSearchQuery query)
    {
        var expeditionsWithCount = (await expeditionsReader.GetPageAsync(query.UserId, query.PageNumber, query.PageSize)).ToList();

        return new PaginatedResult<Expedition>(expeditionsWithCount, expeditionsWithCount.FirstOrDefault()?.TotalCount ?? 0, query);
    }
}