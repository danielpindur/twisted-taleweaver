using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Expeditions.Entities.Enums;

namespace TwistedTaleweaver.Gateway.Expeditions.Contracts;

/// <summary>
/// Request to create a new expedition
/// </summary>
public class ExpeditionRequest
{
    /// <summary>
    /// Name of the expedition
    /// </summary>
    [Length(1, 100)]
    [Required]
    public required string Name { get; set; }
    
    /// <summary>
    /// Description of the expedition to drive the narrative 
    /// </summary>
    [Length(1, int.MaxValue)]
    [Required]
    public required string Description { get; set; }
    
    /// <summary>
    /// Difficulty level of the expedition
    /// </summary>
    [Required]
    public ExpeditionDifficulty Difficulty { get; set; }
    
    /// <summary>
    /// Maximum number of participants allowed in the expedition
    /// </summary>
    [Length(1, byte.MaxValue)]
    [Required]
    public byte MaxParticipants { get; set; }
}