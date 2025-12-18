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
    public Guid CharacterId { get; set; }
    
    /// <summary>
    /// <inheritdoc cref="ClassType"/>
    /// </summary>
    [Required]
    public ClassType Class { get; set; }

    /// <summary>
    /// Abilities the entity can use for narrative purposes - eg. lockpicking
    /// </summary>
    public required List<CreatureAction> NonCombatActions { get; set; }

    /// <summary>
    /// The character's current level
    /// </summary>
    public int Level { get; set; } = 1;
    
    public override int ProficiencyBonus => 2 + (Level - 1) / 4;
}