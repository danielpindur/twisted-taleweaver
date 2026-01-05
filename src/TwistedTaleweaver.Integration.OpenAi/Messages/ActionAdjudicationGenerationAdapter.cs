using System.Text;
using System.Text.Json;
using TwistedTaleweaver.Integration.OpenAi.Creatures.Entities;
using TwistedTaleweaver.Integration.OpenAi.Expeditions.Entities;
using TwistedTaleweaver.Integration.OpenAi.Generators;
using TwistedTaleweaver.Integration.OpenAi.Messages.Entities;

namespace TwistedTaleweaver.Integration.OpenAi.Messages;

public class ActionAdjudicationGenerationCommand
{
    public required string ActionText { get; set; }
    
    public required CharacterContext CharacterPerformingAction { get; set; }
    
    public required ExpeditionContext Expedition { get; set; }
    
    public required IEnumerable<string> PreviousNarrativeTexts { get; set; }
    
    public required List<CharacterContext> OtherCharacters { get; set; }
    
    // TODO: Add monsters?
}

public interface IActionAdjudicationGenerationAdapter
{
    Task<GeneratedActionAdjudication> GenerateAsync(ActionAdjudicationGenerationCommand command);
}

internal class ActionAdjudicationAdjudicationGenerationAdapter(IEntityGenerator entityGenerator) : IActionAdjudicationGenerationAdapter
{
    public async Task<GeneratedActionAdjudication> GenerateAsync(ActionAdjudicationGenerationCommand command)
    {
        var sb = new StringBuilder();

        sb.Append("You are adjudicating a player action in a dark fantasy roleplaying game.\n");

        sb.Append($"Player action: \"{command.ActionText}\".\n");

        sb.Append("Actor details:\n");
        sb.Append(JsonSerializer.Serialize(command.CharacterPerformingAction));
        sb.Append("\n");

        sb.Append("Other characters present:\n");
        sb.Append(JsonSerializer.Serialize(command.OtherCharacters));
        sb.Append("\n");

        sb.Append("Expedition context:\n");
        sb.Append(JsonSerializer.Serialize(command.Expedition));
        sb.Append("\n");

        sb.Append("Recent narrative context:\n");
        foreach (var narrativeText in command.PreviousNarrativeTexts)
        {
            sb.Append($"- {narrativeText}\n");
        }

        sb.Append("""
                  Rules:
                  - Do NOT narrate outcomes.
                  - Determine whether the action is physically and logically possible.
                  - If possible, determine which attribute checks are required.
                  - Attribute checks must use ONLY the provided attributes.
                  - Traits may modify or justify checks but do not auto-succeed unless explicitly stated.
                  - Be strict but fair.
                  - Return structured data only.
                  """);

        return await entityGenerator.GenerateEntityAsync<GeneratedActionAdjudication>(
            sb.ToString(),
            TwistedTaleweaverContextHelper.ActionAdjudicationContext);
    }
}