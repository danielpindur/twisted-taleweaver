using System.ComponentModel.DataAnnotations;

namespace TwistedTaleweaver.Domain.Creatures;

/// <summary>
/// Passive ability or feature that is always active or trigger under certain conditions
/// </summary>
public class Trait
{
    /// <summary>
    /// Name of the trait
    /// </summary>
    [MaxLength(50)]
    [Required]
    public required string Name { get; init; }

    /// <summary>
    /// Description of the trait's effect or purpose
    /// </summary>
    [MaxLength(255)]
    [Required]
    public required string Effect { get; init; }

    /// <summary>
    /// Optional. Condition under which the trait is active or applies
    /// </summary>
    [MaxLength(255)]
    public string? ConditionForPerform { get; init; }
}