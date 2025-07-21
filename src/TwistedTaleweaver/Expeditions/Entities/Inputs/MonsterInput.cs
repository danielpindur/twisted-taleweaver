namespace TwistedTaleweaver.Expeditions.Entities.Inputs;

public class MonsterInput
{
    public Guid MonsterId { get; init; }

    public required string Name { get; init; }
    
    public int Health { get; set; }
}