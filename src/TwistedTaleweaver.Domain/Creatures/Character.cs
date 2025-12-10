using TwistedTaleweaver.Domain.Creatures.Enums;

namespace TwistedTaleweaver.Domain.Creatures;

/// <summary>
/// Represents an user's character within the system
/// </summary>
public class Character : CombatCreature
{
    /// <summary>
    /// <inheritdoc cref="ClassType"/>
    /// </summary>
    public ClassType Class { get; init; }

    /// <summary>
    /// Abilities the entity can use for narrative purposes - eg. lockpicking
    /// </summary>
    public required List<Action> NonCombatActions { get; init; }

    /// <summary>
    /// The character's current level
    /// </summary>
    public int Level { get; set; } = 1;
    
    public override int ProficiencyBonus => 2 + (Level - 1) / 4;
}