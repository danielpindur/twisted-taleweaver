using TwistedTaleweaver.Core.Pagination;
using TwistedTaleweaver.Gateway.Common.Pagination.Contracts;

namespace TwistedTaleweaver.Gateway.Common.Pagination.Mappers;

internal static class PaginationMapper
{
    public static OffsetPage<TResponse> ToContract<TResult, TResponse>(
        this PaginatedResult<TResult> source, Func<TResult, TResponse> map)
    {
        return new OffsetPage<TResponse>()
        {
            Items = source.Items.Select(map).ToList(),
            Page = source.PageNumber,
            PageSize = source.PageSize,
            TotalCount = source.TotalItems
        };
    }
}