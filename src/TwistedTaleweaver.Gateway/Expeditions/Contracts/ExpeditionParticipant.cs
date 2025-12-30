namespace TwistedTaleweaver.Gateway.Expeditions.Contracts;

[GraphQLDescription("Represents a participant in an expedition.")]
public class ExpeditionParticipant
{
    [GraphQLIgnore]
    public Guid UserId { get; init; }
    
    [GraphQLIgnore]
    public Guid? CharacterId { get; set; }
}