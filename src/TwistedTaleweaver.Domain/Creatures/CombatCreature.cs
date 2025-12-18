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
    public int MaxHealth { get; set; }

    /// <summary>
    /// Remaining hit points
    /// </summary>
    [Range(1, 1000)]
    public int CurrentHealth { get; set; }

    /// <summary>
    /// Determines how well-protected the entity is against standard attacks
    /// </summary>
    [Range(1, 30)]
    public int ArmorClass { get; set; }
    
    /// <summary>
    /// <inheritdoc cref="SpeedScores"/>
    /// </summary>
    public required SpeedScores SpeedScores  { get; set; }
    
    /// <summary>
    /// Abilities the entity can use on its turn during combat
    /// </summary>
    public required List<CreatureAction> CombatActions { get; set; }

    /// <summary>
    /// Represents the proficiency bonus for the character, which is added to attack rolls and saving throws if applicable.
    /// </summary>
    [JsonIgnore]
    public abstract int ProficiencyBonus { get; }
}