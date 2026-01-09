using System.ComponentModel.DataAnnotations;

namespace TwistedTaleweaver.Integration.OpenAi.Messages.Entities;

/// <summary>
/// Represents a trait that has been applied and affected the outcome of an action
/// </summary>
public class AppliedTrait
{
    /// <summary>
    /// Name of the trait applied
    /// </summary>
    [MaxLength(50)]
    [Required]
    public required string TraitName { get; set; }
    
    /// <summary>
    /// Summary of the effect of the applied trait
    /// </summary>
    [MaxLength(255)]
    [Required]
    public required string EffectSummary { get; set; }
}