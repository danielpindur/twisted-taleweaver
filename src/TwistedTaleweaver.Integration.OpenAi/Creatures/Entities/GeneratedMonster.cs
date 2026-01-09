using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Api.Creatures.Entities.Enums;

namespace TwistedTaleweaver.Integration.OpenAi.Creatures.Entities;

/// <summary>
/// Generated monster the expeditioners have encountered
/// </summary>
public class GeneratedMonster
{
    /// <summary>
    /// Name of the monster
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
    /// Type of a creature, that the monster is.
    /// </summary>
    public CreatureType CreatureType { get; set; }
    
    /// <summary>
    /// Narrative alignment of the monster.
    /// </summary>
    public NarrativeAlignment NarrativeAlignment { get; set; }
    
    /// <summary>
    /// Gender of the monster.
    /// </summary>
    public CreatureGender CreatureGender { get; set; }
    
    /// <summary>
    /// Breakdown of the monster's core attributes and their scores.
    /// </summary>
    public required AttributeScores AttributeScores { get; set; }
    
    /// <summary>
    /// Maximum health points of the monster - depends on monster challenge rating and constitution attribute
    /// </summary>
    [Range(294, 1000)]
    public int MaxHealth { get; set;}
    
    /// <summary>
    /// 
    /// </summary>
    [Range(0, 30)]
    public int ChallengeRating { get; set;}
}