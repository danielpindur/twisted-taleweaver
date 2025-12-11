namespace TwistedTaleweaver.Core.Pagination;

public static class PaginationHelper
{
    public static int CalculateOffset(int pageNumber, int pageSize)
    {
        return (pageNumber - 1) * pageSize;
    }
}