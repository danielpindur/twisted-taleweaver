namespace TwistedTaleweaver.Gateway.Expeditions.Contracts;

[GraphQLDescription("Request to start an existing expedition.")]
public class StartExpeditionInput
{
    [GraphQLDescription("Unique identifier of the expedition to start.")]
    public Guid ExpeditionId { get; set; }
}