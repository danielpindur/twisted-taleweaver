namespace TwistedTaleweaver.Gateway.Common.Pagination.Contracts;

/// <summary>
/// Base class for pagination query parameters
/// </summary>
public abstract class PaginationQuery
{
    private const int MaxPageSize = 100;
    private const int MinPageNumber = 1;

    /// <summary>
    /// Page number to retrieve (1-based)
    /// </summary>
    public int PageNumber
    {
        get;
        set => field = value < MinPageNumber ? MinPageNumber : value;
    } = 1;

    /// <summary>
    /// Number of items per page
    /// </summary>
    public int PageSize
    {
        get;
        set => field = value > MaxPageSize ? MaxPageSize : value;
    } = 10;
}