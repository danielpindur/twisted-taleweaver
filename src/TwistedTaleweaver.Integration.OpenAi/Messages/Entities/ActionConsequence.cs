using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Integration.OpenAi.Messages.Entities.Enums;

namespace TwistedTaleweaver.Integration.OpenAi.Messages.Entities;

/// <summary>
/// Represents a consequence resulting from an action taken
/// </summary>
public class ActionConsequence
{
    /// <summary>
    /// Description of the consequence
    /// </summary>
    [MaxLength(255)]
    [Required]
    public required string Description { get; set; }
    
    /// <summary>
    /// Severity level of the consequence
    /// </summary>
    public ConsequenceSeverity Severity { get; set; }
}