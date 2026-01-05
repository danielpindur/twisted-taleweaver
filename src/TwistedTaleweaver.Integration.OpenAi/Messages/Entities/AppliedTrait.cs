using System.ComponentModel.DataAnnotations;

namespace TwistedTaleweaver.Integration.OpenAi.Messages.Entities;

public class AppliedTrait
{
    [MaxLength(50)]
    [Required]
    public required string TraitName { get; set; }
    
    [MaxLength(255)]
    [Required]
    public required string EffectSummary { get; set; }
}