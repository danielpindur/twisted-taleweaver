using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Domain.Creatures.Enums;

namespace TwistedTaleweaver.Domain.Creatures;

/// <summary>
/// Represents a character or monster's proficiency in a specific skill, weapon, tool, or other area
/// </summary>
public class Proficiency
{
    /// <summary>
    /// <inheritdoc cref="ProficiencyType"/>
    /// </summary>
    public ProficiencyType Type { get; init; }

    /// <summary>
    /// The specific target of the proficiency, e.g., "Stealth", "Longsword", "Dexterity"
    /// </summary>
    [MaxLength(50)]
    public required string Target { get; init; }
}