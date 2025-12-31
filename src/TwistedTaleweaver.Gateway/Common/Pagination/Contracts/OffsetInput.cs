namespace TwistedTaleweaver.Gateway.Common.Pagination.Contracts;

[GraphQLDescription("Input for offset pagination.")]
public class OffsetInput
{
    [GraphQLDescription("Current page number (1-based).")]
    public int Page { get; init; } = 1;
    
    [GraphQLDescription("Number of items per page.")]
    public int PageSize { get; init; } = 10;
}