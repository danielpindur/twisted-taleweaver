namespace TwistedTaleweaver.Gateway.Common.Pagination.Contracts;

[GraphQLDescription("Represents a page of items using offset pagination.")]
public class OffsetPage<T>
{
    [GraphQLDescription("The items on the current page.")]
    public required IReadOnlyList<T> Items { get; init; }
    
    [GraphQLDescription("Total number of items available.")]
    public required int TotalCount { get; init; }
    
    [GraphQLDescription("Current page number (1-based).")]
    public required int Page { get; init; }
    
    [GraphQLDescription("Number of items per page.")]
    public required int PageSize { get; init; }

    [GraphQLDescription("Total number of pages available.")]
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
