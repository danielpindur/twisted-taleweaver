using System.ComponentModel.DataAnnotations;

namespace TwistedTaleweaver.Api.Creatures.Entities;

/// <summary>
/// Shared base class for all combat-capable creatures
/// </summary>
public abstract class CombatCreature : Creature
{
    /// <summary>
    /// Total hit points
    /// </summary>
    [Range(1, 1000)]
    public int MaxHealth { get; set; }

    /// <summary>
    /// Determines how well-protected the entity is against standard attacks
    /// </summary>
    [Range(1, 30)]
    public int ArmorClass { get; set; }

    /// <summary>
    /// <inheritdoc cref="SpeedScores"/>
    /// </summary>
    public required SpeedScores SpeedScores { get; set; }
}