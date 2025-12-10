using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Domain.Creatures.Enums;

namespace TwistedTaleweaver.Domain.Creatures;

/// <summary>
/// Shared base class for all creatures
/// </summary>
public abstract class Creature
{
    /// <summary>
    /// Name of the entity (e.g., monster name, character title)
    /// </summary>
    [MaxLength(50)]
    [Required]
    public required string Name { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="CreatureStatus"/>
    /// </summary>
    [Required]
    public CreatureStatus Status { get; set; } = CreatureStatus.Alive;

    /// <summary>
    /// <inheritdoc cref="AttributeScores"/>
    /// </summary>
    [Required]
    public required AttributeScores AttributeScores { get; set; }

    /// <summary>
    /// Context string for AI generation (e.g. flavor/lore)
    /// </summary>
    [MaxLength(255)]
    [Required]
    public required string ContextKeywords { get; set; }
    
    /// <summary>
    /// <inheritdoc cref="CreatureType"/>
    /// </summary>
    [Required]
    public CreatureType CreatureType { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="NarrativeAlignment"/>
    /// </summary>
    [Required]
    public NarrativeAlignment Alignment { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="Trait"/>
    /// </summary>
    [Required]
    public required List<Trait> Traits { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="Proficiency"/>
    /// </summary>
    [Required]
    public required List<Proficiency> Proficiencies { get; init; }
}