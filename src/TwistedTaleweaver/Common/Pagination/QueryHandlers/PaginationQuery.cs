namespace TwistedTaleweaver.Common.Pagination.QueryHandlers;

public abstract class PaginationQuery
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }
    
    public int Offset => PageNumber * PageSize;
}