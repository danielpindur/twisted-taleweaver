using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TwistedTaleweaver.Domain.Creatures.Enums;

namespace TwistedTaleweaver.Domain.Creatures;

/// <summary>
/// Breakdown of a character's attribute scores that represents their physical and mental capabilities.
/// </summary>
public class AttributeScores
{
    /// <summary>
    /// Strength represents a character's physical power and ability to exert force.
    /// </summary>
    [Range(1, 20)]
    public int Strength { get; init; }
    
    /// <summary>
    /// Dexterity represents a character's agility, reflexes, and balance.
    /// </summary>
    [Range(1, 20)]
    public int Dexterity { get; init; }
    
    /// <summary>
    /// Constitution represents a character's health, stamina, and resilience against physical challenges.
    /// </summary>
    [Range(1, 20)]
    public int Constitution { get; init; }
    
    /// <summary>
    /// Intelligence represents a character's mental acuity, knowledge, and reasoning ability.
    /// </summary>
    [Range(1, 20)]
    public int Intelligence { get; init; }
    
    /// <summary>
    /// Wisdom represents a character's perception, insight, and intuition about the world around them.
    /// </summary>
    [Range(1, 20)]
    public int Wisdom { get; init; }
    
    /// <summary>
    /// Charisma represents a character's charm, social skills, and ability to influence others.
    /// </summary>
    [Range(1, 20)]
    public int Charisma { get; init; }

    [JsonIgnore]
    [field: JsonIgnore]
    public Dictionary<AttributeType, int> Bonuses => field ??= new()
    {
        [AttributeType.Strength] = (Strength - 10) / 2,
        [AttributeType.Dexterity] = (Dexterity - 10) / 2,
        [AttributeType.Constitution] = (Constitution - 10) / 2,
        [AttributeType.Intelligence] = (Intelligence - 10) / 2,
        [AttributeType.Wisdom] = (Wisdom - 10) / 2,
        [AttributeType.Charisma] = (Charisma - 10) / 2
    };
}