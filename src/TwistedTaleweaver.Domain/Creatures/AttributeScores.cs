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
    [Required]
    public int Strength { get; set; }
    
    /// <summary>
    /// Dexterity represents a character's agility, reflexes, and balance.
    /// </summary>
    [Range(1, 20)]
    [Required]
    public int Dexterity { get; set; }
    
    /// <summary>
    /// Constitution represents a character's health, stamina, and resilience against physical challenges.
    /// </summary>
    [Range(1, 20)]
    [Required]
    public int Constitution { get; set; }
    
    /// <summary>
    /// Intelligence represents a character's mental acuity, knowledge, and reasoning ability.
    /// </summary>
    [Range(1, 20)]
    [Required]
    public int Intelligence { get; set; }
    
    /// <summary>
    /// Wisdom represents a character's perception, insight, and intuition about the world around them.
    /// </summary>
    [Range(1, 20)]
    [Required]
    public int Wisdom { get; set; }
    
    /// <summary>
    /// Charisma represents a character's charm, social skills, and ability to influence others.
    /// </summary>
    [Range(1, 20)]
    [Required]
    public int Charisma { get; set; }
}