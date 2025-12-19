using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TwistedTaleweaver.Domain.Creatures;

/// <summary>
/// Shared base class for all combat-capable creatures
/// </summary>
public abstract class CombatCreature : Creature
{
    /// <summary>
    /// Total hit points
    /// </summary>
    [Range(1, 1000)]
    [Required]
    public int MaxHealth { get; set; }

    /// <summary>
    /// Determines how well-protected the entity is against standard attacks
    /// </summary>
    [Range(1, 30)]
    [Required]
    public int ArmorClass { get; set; }
    
    /// <summary>
    /// <inheritdoc cref="SpeedScores"/>
    /// </summary>
    [Required]
    public required SpeedScores SpeedScores  { get; set; }
}