using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Domain.Creatures.Enums;

namespace TwistedTaleweaver.Domain.Creatures;

/// <summary>
/// Represents current state of a combat creature
/// </summary>
public class CombatCreatureState
{
    /// <summary>
    /// Identifier of the creature this state data is related to
    /// </summary>
    [Required]
    public Guid CreatureId { get; set; }

    /// <summary>
    /// <see cref="CreatureStatus"/>
    /// </summary>
    [Required]
    public CreatureStatus Status { get; set; }

    /// <summary>
    /// Remaining hit points
    /// </summary>
    [Range(1, 1000)]
    [Required]
    public int CurrentHealth { get; set; }
}