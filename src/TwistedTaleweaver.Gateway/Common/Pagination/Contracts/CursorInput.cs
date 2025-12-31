namespace TwistedTaleweaver.Gateway.Common.Pagination.Contracts;

[GraphQLDescription("Input for cursor-based pagination.")]
public class CursorInput<TCursor> where TCursor : struct
{
    [GraphQLDescription("The cursor after which to start fetching items.")]
    public TCursor? After { get; set; }
    
    [GraphQLDescription("Number of items to fetch.")]
    public int PageSize { get; set; } = 10;
}