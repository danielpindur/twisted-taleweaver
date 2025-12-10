using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Domain.Expeditions.Enums;

namespace TwistedTaleweaver.Gateway.Expeditions.Contracts;

/// <summary>
/// Request to create a new expedition
/// </summary>
public class ExpeditionRequest
{
    /// <summary>
    /// Name of the expedition - if empty a name will be generated
    /// </summary>
    [Length(1, 100)]
    public string? Name { get; set; }
    
    /// <summary>
    /// Description of the expedition to drive the narrative - if empty a description will be generated
    /// </summary>
    [Length(1, int.MaxValue)]
    public string? Description { get; set; }
    
    /// <summary>
    /// Difficulty level of the expedition
    /// </summary>
    [Required]
    public ExpeditionDifficulty Difficulty { get; set; }
    
    /// <summary>
    /// Maximum number of participants allowed in the expedition
    /// </summary>
    [Range(1, 10)]
    [Required]
    public byte MaxParticipants { get; set; }
}