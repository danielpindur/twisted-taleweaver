using TwistedTaleweaver.Domain.Creatures.Enums;

namespace TwistedTaleweaver.DataAccess.Creatures.Dtos;

public class CharacterDto
{
    public Guid CreatureId { get; set; }
    
    public required string Name { get; set; }
    
    public required string ContextKeywords { get; set; }
    
    public CreatureType CreatureType { get; set; }
    
    public NarrativeAlignment Alignment { get; set; }
    
    public Gender Gender { get; set; }
    
    public int Strength { get; set; }

    public int Dexterity { get; set; }
    
    public int Constitution { get; set; }
    
    public int Intelligence { get; set; }
    
    public int Wisdom { get; set; }
    
    public int Charisma { get; set; }
    
    public int MaxHealth { get; set; }
    
    public int ArmorClass { get; set; }
    
    public int WalkingSpeed { get; init; }
    
    public int? FlyingSpeed { get; init; }
    
    public int? SwimmingSpeed { get; init; }
    
    public int? ClimbingSpeed { get; init; }
    
    public int? BurrowingSpeed { get; init; }
    
    public int? TeleportationSpeed { get; init; }
    
    public ClassType Class { get; set; }
}