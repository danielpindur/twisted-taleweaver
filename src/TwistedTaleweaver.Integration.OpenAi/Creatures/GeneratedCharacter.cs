using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Domain.Creatures;
namespace TwistedTaleweaver.Integration.OpenAi.Creatures;

/// <summary>
/// Generated playable character
/// </summary>
public class GeneratedCharacter
{
    /// <summary>
    /// Name of the character
    /// </summary>
    [MaxLength(50)]
    public required string Name { get; set; }
    
    /// <summary>
    /// Summary context string to drive AI generation - contains information not contained in other properties
    /// </summary>
    [MaxLength(255)]
    public required string CharacterContextKeywords { get; set; }
    
    /// <summary>
    /// <inheritdoc cref="GeneratedTrait"/>
    /// </summary>
    [Length(0, 4)]
    public required List<GeneratedTrait> Traits { get; set; }
    
    /// <summary>
    /// <inheritdoc cref="Proficiency"/>
    /// </summary>
    [Length(0, 4)]
    public required List<Proficiency> Proficiencies { get; set; }
    
    /// <summary>
    /// Determines how well-protected the entity is against standard attacks
    /// </summary>
    [Range(1, 30)]
    public int ArmorClass { get; set; }
    
    /// <summary>
    /// <inheritdoc cref="GeneratedSpeedScores"/>
    /// </summary>
    public required GeneratedSpeedScores SpeedScores  { get; set; }
    
    /// <summary>
    /// Abilities the entity can use on its turn during combat
    /// </summary>
    [Length(0, 4)]
    public required List<GeneratedCreatureAction> CombatActions { get; set; }
    
    /// <summary>
    /// Abilities the entity can use for narrative purposes - eg. lockpicking
    /// </summary>
    [Length(0, 4)]
    public required List<GeneratedCreatureAction> NonCombatActions { get; set; }
}