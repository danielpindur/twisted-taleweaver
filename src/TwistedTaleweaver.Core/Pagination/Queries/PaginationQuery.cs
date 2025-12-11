namespace TwistedTaleweaver.Core.Pagination.Queries;

public abstract class PaginationQuery
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}