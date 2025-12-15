using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.Core.Pagination;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;
using TwistedTaleweaver.Domain.Expeditions;
using TwistedTaleweaver.Expeditions.Mappers;

namespace TwistedTaleweaver.Expeditions.QueryHandlers;

internal class ExpeditionSearchQueryHandler(IExpeditionsReader expeditionsReader) : IExpeditionSearchQueryHandler
{
    public async Task<PaginatedResult<ExpeditionSlim>> ExecuteAsync(ExpeditionSearchQuery query)
    {
        var dtos = (await expeditionsReader.GetPageAsync(query.UserId, query.PageNumber, query.PageSize)).ToList();
        
        return new PaginatedResult<ExpeditionSlim>(dtos.ToExpeditionSlims(), dtos.FirstOrDefault()?.TotalCount ?? 0, query);
    }
}