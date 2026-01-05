using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Integration.OpenAi.Messages.Entities.Enums;

namespace TwistedTaleweaver.Integration.OpenAi.Messages.Entities;

public class ActionConsequence
{
    [MaxLength(255)]
    [Required]
    public required string Description { get; set; }
    
    public ConsequenceSeverity Severity { get; set; }
}