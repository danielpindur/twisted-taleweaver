using TwistedTaleweaver.Api.Expeditions.Entities.Enums;
using TwistedTaleweaver.Gateway.Creatures.Contracts;

namespace TwistedTaleweaver.Gateway.Expeditions.Contracts;

[GraphQLDescription("Information regarding the state the expedition is in.")]
public class ExpeditionState
{
    [GraphQLIgnore]
    public Guid ExpeditionId { get; set; }
    
    [GraphQLDescription("Current status of the expedition.")]
    public ExpeditionStatus Status { get; init; }
    
    [GraphQLIgnore]
    public Guid? ActiveCreatureId { get; init; }
    
    [GraphQLDescription("List of monsters currently present in the expedition.")]
    public required List<Monster> Monsters { get; init; }
}