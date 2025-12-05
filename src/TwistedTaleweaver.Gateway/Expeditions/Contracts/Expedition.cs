using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Expeditions.Entities.Enums;

namespace TwistedTaleweaver.Gateway.Expeditions.Contracts;

/// <summary>
/// Represents an expedition within the system
/// </summary>
public class Expedition
{
    /// <summary>
    /// Unique identifier of the expedition
    /// </summary>
    [Required]
    public Guid ExpeditionId { get; set; }
    
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
    /// Current status of the expedition
    /// </summary>
    [Required]
    public ExpeditionStatus Status { get; set; }
    
    /// <summary>
    /// Difficulty level of the expedition
    /// </summary>
    [Required]
    public ExpeditionDifficulty  Difficulty { get; set; }
    
    /// <summary>
    /// Maximum number of participants allowed in the expedition
    /// </summary>
    [Range(1, 10)]
    [Required]
    public byte MaxParticipants { get; set; }

    /// <summary>
    /// Outcome of the expedition, if completed
    /// </summary>    
    public ExpeditionOutcome? Outcome { get; set; }
    
    // TODO: User created

    /// <summary>
    /// Date and time when the expedition was created
    /// </summary>
    [Required]
    public DateTimeOffset CreatedAt { get; set; }
    
    /// <summary>
    /// Date and time when the expedition was last updated
    /// </summary>
    [Required]
    public DateTimeOffset UpdatedAt { get; set; }
    
    /// <summary>
    /// Date and time when the expedition started
    /// </summary>
    public DateTimeOffset? StartedAt { get; set; }
    
    /// <summary>
    /// Date and time when the expedition was completed
    /// </summary>
    public DateTimeOffset? CompletedAt { get; set; }
    
    // TODO: Participants
}