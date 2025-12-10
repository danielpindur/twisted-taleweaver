using System.ComponentModel.DataAnnotations;

namespace TwistedTaleweaver.Integration.OpenAi.Expeditions;

/// <summary>
/// Generated expedition entity
/// </summary>
public class GeneratedExpedition
{
    /// <summary>
    /// Name of the expedition
    /// </summary>
    [MaxLength(50)]
    [Required]
    public required string Name { get; set; }
    
    /// <summary>
    /// Description of the expedition to drive the narrative
    /// </summary>
    [MaxLength(500)]
    [Required]
    public required string Description { get; set; }
}