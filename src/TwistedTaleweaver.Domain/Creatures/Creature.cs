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
    public required string Name { get; set; }

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
    /// <inheritdoc cref="Gender"/>
    /// </summary>
    [Required]
    public Gender Gender { get; set; }
    
    /// <summary>
    /// <inheritdoc cref="CreatureType"/>
    /// </summary>
    [Required]
    public CreatureType CreatureType { get; set; }
    
    /// <summary>
    /// <inheritdoc cref="NarrativeAlignment"/>
    /// </summary>
    [Required]
    public NarrativeAlignment Alignment { get; set; }
    
    /// <summary>
    /// <inheritdoc cref="Trait"/>
    /// </summary>
    [Required]
    public required List<Trait> Traits { get; set; }
}