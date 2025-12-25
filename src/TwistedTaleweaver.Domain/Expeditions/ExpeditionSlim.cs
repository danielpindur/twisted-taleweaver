using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Domain.Expeditions.Enums;

namespace TwistedTaleweaver.Domain.Expeditions;

/// <summary>
/// Represents a slim version of an expedition within the system
/// </summary>
public class ExpeditionSlim
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
    public ExpeditionDifficulty Difficulty { get; set; }

    /// <summary>
    /// Minimum number of participants required to start the expedition
    /// </summary>
    [Range(1, 10)]
    [Required]
    public int MinParticipants { get; set; }

    /// <summary>
    /// Maximum number of participants allowed in the expedition
    /// </summary>
    [Range(1, 10)]
    [Required]
    public int MaxParticipants { get; set; }

    /// <summary>
    /// Current number of participants in the expedition
    /// </summary>
    [Range(0, 10)]
    [Required]
    public int CurrentParticipants { get; set; }

    /// <summary>
    /// Outcome of the expedition, if completed
    /// </summary>    
    public ExpeditionOutcome? Outcome { get; set; }
}