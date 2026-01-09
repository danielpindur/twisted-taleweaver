using TwistedTaleweaver.Api.Expeditions.Entities.Enums;
using TwistedTaleweaver.Gateway.Common.Pagination.Contracts;

namespace TwistedTaleweaver.Gateway.Expeditions.Contracts;

[GraphQLDescription("Input for searching expeditions with pagination and status filters.")]
public class ExpeditionSearchInput : OffsetInput
{
    [GraphQLDescription("List of expedition statuses to filter the search results.")]
    public required List<ExpeditionStatus> Statuses { get; set; }
    
    [GraphQLDescription("Filter expeditions by their difficulty level.")]
    public ExpeditionDifficulty? Difficulty { get; set; }
    
    [GraphQLDescription("Filter expeditions by their outcome.")]
    public ExpeditionOutcome? Outcome { get; set; }
    
    [GraphQLDescription("Text to search for in expedition names or descriptions.")]
    public string? SearchText { get; set; }
}