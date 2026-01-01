using TwistedTaleweaver.Api.Creatures.Entities.Enums;

namespace TwistedTaleweaver.Api.Creatures.Entities;

/// <summary>
/// Represents an user's character within the system
/// </summary>
public class Character : CombatCreature
{
    /// <summary>
    /// <inheritdoc cref="CharacterClass"/>
    /// </summary>
    public CharacterClass Class { get; set; }
}