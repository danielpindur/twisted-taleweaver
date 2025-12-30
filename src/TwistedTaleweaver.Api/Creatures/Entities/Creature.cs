using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Api.Creatures.Entities.Enums;

namespace TwistedTaleweaver.Api.Creatures.Entities;

/// <summary>
/// Shared base class for all creatures
/// </summary>
public abstract class Creature
{
    /// <summary>
    /// Name of the entity (e.g., monster name, character title)
    /// </summary>
    [MaxLength(50)]
    public required string Name { get; set; }

    /// <summary>
    /// <inheritdoc cref="AttributeScores"/>
    /// </summary>
    public required AttributeScores AttributeScores { get; set; }

    /// <summary>
    /// Context string for AI generation (e.g. flavor/lore)
    /// </summary>
    [MaxLength(255)]
    public required string ContextKeywords { get; set; }

    /// <summary>
    /// <inheritdoc cref="CreatureGender"/>
    /// </summary>
    public CreatureGender Gender { get; set; }

    /// <summary>
    /// <inheritdoc cref="CreatureType"/>
    /// </summary>
    public CreatureType CreatureType { get; set; }

    /// <summary>
    /// <inheritdoc cref="NarrativeAlignment"/>
    /// </summary>
    public NarrativeAlignment Alignment { get; set; }
}