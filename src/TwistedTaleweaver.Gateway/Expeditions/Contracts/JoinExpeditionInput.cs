namespace TwistedTaleweaver.Gateway.Expeditions.Contracts;

[GraphQLDescription("Request to join an existing expedition.")]
public class JoinExpeditionInput
{
    [GraphQLDescription("Unique identifier of the expedition to join.")]
    public Guid ExpeditionId { get; set; }
}