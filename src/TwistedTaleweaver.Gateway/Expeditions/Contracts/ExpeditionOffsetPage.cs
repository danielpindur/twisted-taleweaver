using TwistedTaleweaver.Gateway.Common.Pagination.Contracts;

namespace TwistedTaleweaver.Gateway.Expeditions.Contracts;

[GraphQLDescription("Offset paginated expeditions.")]
public class ExpeditionOffsetPage : OffsetPage<Expedition>
{
    [GraphQLIgnore]
    public required ExpeditionSearchInput ExpeditionSearchInput { get; set; }
}