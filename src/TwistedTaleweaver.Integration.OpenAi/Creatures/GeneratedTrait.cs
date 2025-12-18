using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Domain.Common;

namespace TwistedTaleweaver.Integration.OpenAi.Creatures;

/// <summary>
/// Passive ability or feature that is always active or trigger under certain conditions
/// </summary>
public class GeneratedTrait
{
    /// <summary>
    /// Name of the trait
    /// </summary>
    [MaxLength(50)]
    public required string Name { get; init; }
    
    /// <summary>
    /// Description of the trait's effect or purpose
    /// </summary>
    [MaxLength(255)]
    public required string Effect { get; init; }
    
    /// <summary>
    /// Optional. Condition under which the trait is active or applies
    /// </summary>
    [MaxLength(255)] 
    public required Optional<string> ConditionForPerform { get; init; }
}