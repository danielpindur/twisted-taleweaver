namespace TwistedTaleweaver.Expeditions.Entities.Inputs;

public class CharacterInput
{
    public Guid CharacterId { get; init; }
    
    public required string ExternalUserId { get; set; }
    
    public int Health { get; set; }
}