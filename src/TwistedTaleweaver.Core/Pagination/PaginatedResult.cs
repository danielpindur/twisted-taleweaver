using TwistedTaleweaver.Core.Pagination.Queries;

namespace TwistedTaleweaver.Core.Pagination;

public class PaginatedResult<T>
{
    public IEnumerable<T> Items { get; set; }

    public int PageNumber { get; set; }
    
    public int PageSize { get; set; }

    public int TotalItems { get; set; }

    public PaginatedResult(IEnumerable<T> items, int count, PaginationQuery query)
    {
        Items = items;
        TotalItems = count;
        PageNumber = query.PageNumber;
        PageSize = query.PageSize;
    }
}