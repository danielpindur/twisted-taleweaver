using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Api.Expeditions.Entities.Enums;

namespace TwistedTaleweaver.Api.Expeditions.Entities;

/// <summary>
/// Represents an expedition within the system
/// </summary>
public class Expedition
{
    /// <summary>
    /// Unique identifier of the expedition
    /// </summary>
    public Guid ExpeditionId { get; set; }

    /// <summary>
    /// Name of the expedition
    /// </summary>
    [Length(1, 50)]
    public required string Name { get; set; }

    /// <summary>
    /// Description of the expedition to drive the narrative
    /// </summary>
    [MinLength(1)]
    public required string Description { get; set; }

    /// <summary>
    /// Difficulty level of the expedition
    /// </summary>
    public ExpeditionDifficulty Difficulty { get; set; }

    /// <summary>
    /// Minimum number of participants required to start the expedition
    /// </summary>
    [Range(1, 10)]
    public int MinParticipants { get; set; }

    /// <summary>
    /// Maximum number of participants allowed in the expedition
    /// </summary>
    [Range(1, 10)]
    public int MaxParticipants { get; set; }

    /// <summary>
    /// Current number of participants in the expedition
    /// </summary>
    [Range(1, 10)]
    public int CurrentParticipants { get; set; }

    /// <summary>
    /// Outcome of the expedition, if completed
    /// </summary>    
    public ExpeditionOutcome? Outcome { get; set; }
    
    /// <summary>
    /// Lesson learned from the expedition, if any
    /// </summary>
    public string? LessonLearned { get; init; }

    /// <summary>
    /// User who created the expedition
    /// </summary>
    public Guid CreatedByUserId { get; set; }
    
    /// <summary>
    /// Date and time when the expedition was created
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }
    
    /// <summary>
    /// Date and time when the expedition was started, if applicable
    /// </summary>
    public DateTimeOffset? StartedAt { get; set; }
    
    /// <summary>
    /// Date and time when the expedition was completed, if applicable
    /// </summary>
    public DateTimeOffset? CompletedAt { get; set; }
}