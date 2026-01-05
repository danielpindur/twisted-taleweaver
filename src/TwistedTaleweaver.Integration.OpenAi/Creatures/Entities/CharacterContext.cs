using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Api.Creatures.Entities.Enums;

namespace TwistedTaleweaver.Integration.OpenAi.Creatures.Entities;

public class CharacterContext
{
    public Guid CreatureId { get; set; }
    
    public required string Name { get; set; }
    
    public required string Description { get; set; }
    
    public CreatureGender CreatureGender { get; set; }
    
    public NarrativeAlignment NarrativeAlignment { get; set; }
    
    public CreatureType CreatureType { get; set; }
    
    public CharacterClass CharacterClass { get; set; }
    
    public int MaxHealth { get; set; }
    
    public int CurrentHealth { get; set; }
    
    public CreatureStatus Status { get; set; }
    
    public int ArmorClass { get; set; }
    
    public required SpeedScores SpeedScores { get; set; }
    
    public required AttributeScores AttributeScores { get; set; }
    
    public required List<CreatureTrait> Traits { get; set; }
}