using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Domain.Common;
using TwistedTaleweaver.Integration.OpenAi.Messages.Entities.Enums;

namespace TwistedTaleweaver.Integration.OpenAi.Messages.Entities;

/// <summary>
/// Adjudication of whether an action taken by monsters within an expedition is possible, along with reasoning, required checks, applied traits, risk level, and potential consequences
/// </summary>
public class GeneratedMonsterActionAdjudication
{
    /// <summary>
    /// Description of the action being adjudicated
    /// </summary>
    [MaxLength(255)]
    [Required]
    public required string ShortActionDescription { get; set; }

    /// <summary>
    /// List of checks that need to be performed to determine the success of the action
    /// </summary>
    public List<RequiredCheck> RequiredChecks { get; set; } = [];

    /// <summary>
    /// List of traits that are applied to the action
    /// </summary>
    public List<AppliedTrait> AppliedTraits { get; set; } = [];

    /// <summary>
    /// Potential consequences of the action
    /// </summary>
    public required Optional<ActionConsequence> PotentialConsequence { get; set; }
}