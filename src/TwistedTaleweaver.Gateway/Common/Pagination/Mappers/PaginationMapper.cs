using TwistedTaleweaver.Core.Pagination;
using TwistedTaleweaver.Gateway.Common.Pagination.Contracts;
using PaginationQuery = TwistedTaleweaver.Core.Pagination.Queries.PaginationQuery;
using PaginationQueryContract = TwistedTaleweaver.Gateway.Common.Pagination.Contracts.PaginationQuery;

namespace TwistedTaleweaver.Gateway.Common.Pagination.Mappers;

internal static class PaginationMapper
{
    public static TQuery WithPagination<TQuery>(this TQuery query, PaginationQueryContract queryContract)
        where TQuery : PaginationQuery
    {
        query.PageNumber = queryContract.PageNumber;
        query.PageSize = queryContract.PageSize;

        return query;
    }

    public static PaginatedResponse<TResponse> ToContract<TResult, TResponse>(
        this PaginatedResult<TResult> source, Func<TResult, TResponse> map)
    {
        return new PaginatedResponse<TResponse>(
            source.Items.Select(map),
            source.TotalItems,
            source.PageNumber,
            source.PageSize);
    }

    public static PaginatedResponse<T> ToContract<T>(this PaginatedResult<T> source)
    {
        return new PaginatedResponse<T>(
            source.Items,
            source.TotalItems,
            source.PageNumber,
            source.PageSize);
    }
}