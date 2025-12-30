using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Api.Creatures.Entities.Enums;

namespace TwistedTaleweaver.Api.Creatures.Entities;

/// <summary>
/// Represents current state of a combat creature
/// </summary>
public class CombatCreatureState
{
    /// <summary>
    /// Identifier of the creature this state data is related to
    /// </summary>
    public Guid CreatureId { get; set; }

    /// <summary>
    /// <see cref="CreatureStatus"/>
    /// </summary>
    public CreatureStatus Status { get; set; }

    /// <summary>
    /// Remaining hit points
    /// </summary>
    [Range(1, 1000)]
    public int CurrentHealth { get; set; }
}