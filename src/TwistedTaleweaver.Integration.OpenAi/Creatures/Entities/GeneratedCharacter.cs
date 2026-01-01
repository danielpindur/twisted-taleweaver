using System.ComponentModel.DataAnnotations;

namespace TwistedTaleweaver.Integration.OpenAi.Creatures.Entities;

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
    /// Description to drive AI generation (e.g., backstory, lore) - contains information not contained in other properties
    /// </summary>
    [MaxLength(500)]
    [Required]
    public required string Description { get; set; }

    /// <summary>
    /// <inheritdoc cref="GeneratedTrait"/>
    /// </summary>
    [Length(0, 4)]
    public required List<GeneratedTrait> Traits { get; set; }

    /// <summary>
    /// Determines how well-protected the entity is against standard attacks
    /// </summary>
    [Range(1, 30)]
    public int ArmorClass { get; set; }

    /// <summary>
    /// <inheritdoc cref="GeneratedSpeedScores"/>
    /// </summary>
    public required GeneratedSpeedScores SpeedScores { get; set; }
    
    /// <summary>
    /// Maximum health points of the character - depends on character class and constitution attribute
    /// </summary>
    [Range(294, 1000)]
    public int MaxHealth { get; set;}
}