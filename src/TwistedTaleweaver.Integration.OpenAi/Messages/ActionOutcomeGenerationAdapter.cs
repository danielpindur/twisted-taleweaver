using System.Text;
using System.Text.Json;
using TwistedTaleweaver.Api.GameEngine.Entities;
using TwistedTaleweaver.Integration.OpenAi.Creatures.Entities;
using TwistedTaleweaver.Integration.OpenAi.Expeditions.Entities;
using TwistedTaleweaver.Integration.OpenAi.Generators;
using TwistedTaleweaver.Integration.OpenAi.Messages.Entities;

namespace TwistedTaleweaver.Integration.OpenAi.Messages;

public class ActionOutcomeGenerationCommand
{
    public required CharacterContext CharacterPerformingAction { get; set; }
    
    public required ExpeditionContext Expedition { get; set; }
    
    public required IEnumerable<string> PreviousNarrativeTexts { get; set; }
    
    public required List<CharacterContext> OtherCharacters { get; set; }

    // TODO: Add monsters?
    
    public required string ActionAdjudicationMessage { get; set; }
    
    public required List<CheckResult> CheckResults { get; set; }
}

public interface IActionOutcomeGenerationAdapter
{
    Task<GeneratedActionOutcome> GenerateAsync(ActionOutcomeGenerationCommand command);
}


internal class ActionOutcomeGenerationAdapter(IEntityGenerator entityGenerator) : IActionOutcomeGenerationAdapter
{
    public Task<GeneratedActionOutcome> GenerateAsync(ActionOutcomeGenerationCommand command)
    {
        var sb = new StringBuilder();
        
        sb.Append("Action adjudication summary:\n");
        sb.Append(command.ActionAdjudicationMessage);
        sb.Append("\n\n");

        sb.Append("Actor:\n");
        sb.Append(JsonSerializer.Serialize(command.CharacterPerformingAction));
        sb.Append("\n\n");

        sb.Append("Other characters present:\n");
        sb.Append(JsonSerializer.Serialize(command.OtherCharacters));
        sb.Append("\n\n");

        sb.Append("Expedition context:\n");
        sb.Append(JsonSerializer.Serialize(command.Expedition));
        sb.Append("\n\n");

        sb.Append("Recent narrative context:\n");
        foreach (var narrative in command.PreviousNarrativeTexts)
        {
            sb.Append($"- {narrative}\n");
        }

        sb.Append("\nCheck results:\n");
        sb.Append(JsonSerializer.Serialize(command.CheckResults));
        sb.Append("\n\n");

        sb.Append("""
                  Resolution rules:
                  - Use ONLY the provided check results.
                  - A Critical Success allows strong positive effects.
                  - A Success allows limited positive effects.
                  - A Failure allows limited negative or null effects.
                  - A Critical Failure allows strong negative effects.
                  - Effects must be reasonable, grounded, and proportional.
                  - Do NOT introduce new entities.
                  - Do NOT apply effects without a clear causal link to the action.
                  - Health changes must be small unless explicitly justified.
                  - Status changes must use only known CreatureStatus values.
                  - If no meaningful effect occurs, return empty updates.
                  - The short description must be factual, concise, and under 200 characters.
                  """);

        return entityGenerator.GenerateEntityAsync<GeneratedActionOutcome>(
            sb.ToString(),
            TwistedTaleweaverContextHelper.ActionOutcomeContext);
    }
}