namespace TwistedTaleweaver.Gateway.Expeditions.Contracts;

[GraphQLDescription("Represents statistics about expeditions.")]
public class ExpeditionStatistics
{
    [GraphQLDescription("Total number of expeditions.")]
    public int TotalExpeditions { get; set; }
    
    [GraphQLDescription("Number of successful expeditions.")]
    public int SuccessfulExpeditions { get; set; }
    
    [GraphQLDescription("Number of failed expeditions.")]
    public int FailedExpeditions { get; set; }
}