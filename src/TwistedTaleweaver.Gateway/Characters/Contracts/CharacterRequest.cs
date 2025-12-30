using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Api.Creatures.Entities.Enums;

namespace TwistedTaleweaver.Gateway.Characters.Contracts;

/// <summary>
/// Request to create a new character in the expedition
/// </summary>
public class CharacterRequest
{
    /// <summary>
    /// Identifier of the expedition to create the character in
    /// </summary>
    [Required]
    public Guid ExpeditionId { get; set; }

    /// <summary>
    /// Name of the character - if empty a name will be generated
    /// </summary>
    [Length(1, 50)]
    public string? Name { get; set; }

    /// <summary>
    /// Description of the expedition to drive the narrative - if empty a description will be generated
    /// </summary>
    [MinLength(1)]
    public string? Description { get; set; }

    /// <summary>
    /// <inheritdoc cref="ClassType"/>
    /// </summary>
    [Required]
    public CharacterClass Class { get; set; }

    /// <summary>
    /// <inheritdoc cref="Gender"/>
    /// </summary>
    [Required]
    public CreatureGender Gender { get; set; }

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
    /// <inheritdoc cref="AttributeScores"/>
    /// </summary>
    [Required]
    public required AttributeScores AttributeScores { get; set; }
}