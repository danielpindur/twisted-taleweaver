using TwistedTaleweaver.Gateway.Common.Pagination.Contracts;

namespace TwistedTaleweaver.Gateway.Messages.Contracts;

[GraphQLDescription("Input for searching messages within an expedition.")]
public class MessageSearchInput : CursorInput<long>
{
    [GraphQLDescription("Unique identifier of the expedition whose messages are being searched.")]
    public Guid ExpeditionId { get; set; }
}