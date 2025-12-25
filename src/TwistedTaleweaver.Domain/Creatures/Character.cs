using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Domain.Creatures.Enums;

namespace TwistedTaleweaver.Domain.Creatures;

/// <summary>
/// Represents an user's character within the system
/// </summary>
public class Character : CombatCreature
{
    /// <summary>
    /// Identifier of the character
    /// </summary>
    [Required]
    public Guid CharacterId { get; set; }

    /// <summary>
    /// <inheritdoc cref="ClassType"/>
    /// </summary>
    [Required]
    public ClassType Class { get; set; }
}