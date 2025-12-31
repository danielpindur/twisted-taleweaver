namespace TwistedTaleweaver.Gateway.Common.Pagination.Contracts;

[GraphQLDescription("Represents a page of items in cursor-based pagination.")]
public class CursorPage<T, TCursor> where TCursor : struct
{
    [GraphQLDescription("The items on the current page.")]
    public required List<T> Items { get; set; }

    [GraphQLDescription("The cursor pointing to the start of the page.")]
    public TCursor? EndCursor { get; set; }
    
    [GraphQLDescription("Indicates if there is a next page available.")]
    public bool HasNextPage { get; set; }
}