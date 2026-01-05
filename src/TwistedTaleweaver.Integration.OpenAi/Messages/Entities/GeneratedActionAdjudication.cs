using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Domain.Common;
using TwistedTaleweaver.Integration.OpenAi.Messages.Entities.Enums;

namespace TwistedTaleweaver.Integration.OpenAi.Messages.Entities;

public class GeneratedActionAdjudication
{
    public bool IsActionPossible { get; set; }

    [MaxLength(255)]
    [Required]
    public required Optional<string> Reasoning { get; set; }

    public List<RequiredCheck> RequiredChecks { get; set; } = [];

    public List<AppliedTrait> AppliedTraits { get; set; } = [];

    public ActionRiskLevel RiskLevel { get; set; }

    public required Optional<ActionConsequence> PotentialConsequence { get; set; }
}