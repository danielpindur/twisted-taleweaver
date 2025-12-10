namespace TwistedTaleweaver.DataAccess.Common.Pagination.Queries;

public abstract class PaginationQuery
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }
    
    public int Offset => (PageNumber - 1) * PageSize;
}