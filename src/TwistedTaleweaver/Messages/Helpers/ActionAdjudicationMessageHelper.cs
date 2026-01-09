using System.Text;
using TwistedTaleweaver.Core.Common.Extensions;
using TwistedTaleweaver.Integration.OpenAi.Messages.Entities;
using TwistedTaleweaver.Integration.OpenAi.Messages.Entities.Enums;

namespace TwistedTaleweaver.Messages.Helpers;

internal static class ActionAdjudicationMessageHelper
{
    public static string FormatActionAdjudicationMessage(GeneratedCharacterActionAdjudication adjudication)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"**Action:** {adjudication.ShortActionDescription}");
        sb.AppendLine();
        
        sb.AppendLine(adjudication.IsActionPossible
            ? "**Action Possible:** ✅"
            : "**Action Possible:** ❌");

        string riskEmoji = adjudication.RiskLevel switch
        {
            ActionRiskLevel.Trivial => "🟢",
            ActionRiskLevel.Low => "🟡",
            ActionRiskLevel.Moderate => "🟠",
            ActionRiskLevel.High => "🔴",
            ActionRiskLevel.Extreme => "💀",
            _ => ""
        };
        sb.AppendLine($"**Risk Level:** {riskEmoji} {adjudication.RiskLevel.ToString().ToUpper()}");
        sb.AppendLine();

        if (adjudication.Reasoning.HasValue)
        {
            sb.AppendLine("**Reasoning:**");
            sb.AppendLine($"*{adjudication.Reasoning.ActualValue}*");
            sb.AppendLine();
        }

        if (adjudication.RequiredChecks.Count > 0)
        {
            sb.AppendLine("**Required Checks:**");
            foreach (var check in adjudication.RequiredChecks)
            {
                sb.AppendLine($"**{check.Attribute} ({check.Difficulty} - {check.NumberOfDicesToRoll}{check.DiceToRoll.ToDescription()}):** *{check.Justification}*");
            }
            sb.AppendLine();
        }

        if (adjudication.AppliedTraits.Count > 0)
        {
            sb.AppendLine("**Applied Traits:**");
            foreach (var trait in adjudication.AppliedTraits)
            {
                sb.AppendLine($"✨ **{trait.TraitName}:** {trait.EffectSummary}");
            }
            sb.AppendLine();
        }
        
        if (adjudication.PotentialConsequence.HasValue)
        {
            var consequence = adjudication.PotentialConsequence.ActualValue;
            
            string severityEmoji = consequence.Severity switch
            {
                ConsequenceSeverity.Minor => "🟡",
                ConsequenceSeverity.Moderate => "🟠",
                ConsequenceSeverity.Severe => "💀",
                _ => ""
            };

            
            sb.AppendLine("**Potential Consequence:**");
            sb.AppendLine($"**Severity:** {severityEmoji} {consequence.Severity.ToString().ToUpper()}");
            sb.AppendLine($"**Description:** *{consequence.Description}*");
            sb.AppendLine();
        }

        if (!adjudication.IsActionPossible)
        {
            sb.AppendLine("⚠️ *Choose your next action wisely… another attempt may yet succeed if you prepare carefully.*");
        }

        return sb.ToString();
    }

    public static string FormatActionAdjudicationMessage(GeneratedMonsterActionAdjudication adjudication)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"**Action:** {adjudication.ShortActionDescription}");
        sb.AppendLine();

        if (adjudication.RequiredChecks.Count > 0)
        {
            sb.AppendLine("**Required Checks:**");
            foreach (var check in adjudication.RequiredChecks)
            {
                sb.AppendLine($"**{check.Attribute} ({check.Difficulty} - {check.NumberOfDicesToRoll}{check.DiceToRoll.ToDescription()}):** *{check.Justification}*");
            }
            sb.AppendLine();
        }

        if (adjudication.AppliedTraits.Count > 0)
        {
            sb.AppendLine("**Applied Traits:**");
            foreach (var trait in adjudication.AppliedTraits)
            {
                sb.AppendLine($"✨ **{trait.TraitName}:** {trait.EffectSummary}");
            }
            sb.AppendLine();
        }
        
        if (adjudication.PotentialConsequence.HasValue)
        {
            var consequence = adjudication.PotentialConsequence.ActualValue;
            
            string severityEmoji = consequence.Severity switch
            {
                ConsequenceSeverity.Minor => "🟡",
                ConsequenceSeverity.Moderate => "🟠",
                ConsequenceSeverity.Severe => "💀",
                _ => ""
            };

            
            sb.AppendLine("**Potential Consequence:**");
            sb.AppendLine($"**Severity:** {severityEmoji} {consequence.Severity.ToString().ToUpper()}");
            sb.AppendLine($"**Description:** *{consequence.Description}*");
            sb.AppendLine();
        }

        return sb.ToString();
    }
}