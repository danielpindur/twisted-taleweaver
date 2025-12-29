using TwistedTaleweaver.Domain.Expeditions.Enums;

namespace TwistedTaleweaver.Gateway.Expeditions.Contracts;

[GraphQLDescription("Request to create a new expedition.")]
public class AddExpeditionInput
{
    [GraphQLDescription("Name of the expedition - if empty a name will be generated.")]
    public string? Name { get; set; }

    [GraphQLDescription("Description of the expedition to drive the narrative - if empty a description will be generated.")]
    public string? Description { get; set; }

    [GraphQLDescription("Difficulty level of the expedition.")]
    public ExpeditionDifficulty Difficulty { get; set; }

    [GraphQLDescription("Minimum number of participants required to start the expedition.")]
    public int MinParticipants { get; set; }

    [GraphQLDescription("Maximum number of participants allowed in the expedition.")]
    public int MaxParticipants { get; set; }
}