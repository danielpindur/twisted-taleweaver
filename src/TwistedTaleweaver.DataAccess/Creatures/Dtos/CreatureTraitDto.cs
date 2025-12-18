namespace TwistedTaleweaver.DataAccess.Creatures.Dtos;

public class CreatureTraitDto
{
    public Guid CreatureId { get; set; }
    
    public required string TraitName { get; set; }
    
    public required string Effect { get; set; }
    
    public string? ConditionDescription { get; set; }
}