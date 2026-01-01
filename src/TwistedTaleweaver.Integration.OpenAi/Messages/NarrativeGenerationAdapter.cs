using System.Text;
using System.Text.Json;
using TwistedTaleweaver.Integration.OpenAi.Creatures.Entities;
using TwistedTaleweaver.Integration.OpenAi.Expeditions.Entities;
using TwistedTaleweaver.Integration.OpenAi.Generators;
using TwistedTaleweaver.Integration.OpenAi.Messages.Entities;

namespace TwistedTaleweaver.Integration.OpenAi.Messages;

public class NarrativeGenerationCommand
{
    public required ExpeditionContext Expedition { get; set; }
    
    public bool IsFirstNarrativeText { get; set; }
    
    public required IEnumerable<string> PreviousNarrativeTexts { get; set; }
    
    public required List<CharacterContext> Characters { get; set; }
    
    // TODO: Add monsters?
    
    public required IEnumerable<string> ActionMessagesSinceLastNarration { get; set; }
}

public interface INarrativeGenerationAdapter
{
    Task<GeneratedMessage> GenerateAsync(NarrativeGenerationCommand command);
}

internal class NarrativeGenerationAdapter(IEntityGenerator entityGenerator) : INarrativeGenerationAdapter
{
    public async Task<GeneratedMessage> GenerateAsync(NarrativeGenerationCommand command)
    {
        var sb = new StringBuilder();

        sb.Append("Generate the next narrative message for a twisted expedition. ");
        sb.Append($"Expedition Name: \"{command.Expedition.Name}\". ");
        sb.Append($"Expedition Description: \"{command.Expedition.Description}\". ");
        sb.Append($"Expedition Difficulty: \"{command.Expedition.Difficulty.ToString()}\". ");

        sb.Append("Player controlled characters in the expedition: ");
        sb.Append(JsonSerializer.Serialize(command.Characters));
        
        // TODO: Add monsters info
        
        var firstActionText = true;
        foreach (var actionText in command.ActionMessagesSinceLastNarration)
        {
            if (firstActionText)
            {
                firstActionText = false;
                sb.Append("Actions since last narrative generation to generate the narrative for: ");
            }
            
            sb.Append($"\"{actionText}\"; ");
        }

        if (command.IsFirstNarrativeText)
        {
            sb.Append("This is the first narrative message. ");
            sb.Append("Set the scene, introduce the expedition, describe the environment and mood, and hook the adventurers immediately. ");
        }
        
        sb.Append("The narrative should continue the story in a grotesque, darkly comedic, and theatrical style. ");
        sb.Append("Include vivid descriptions, twisted events, and shocking or absurd details. ");
        sb.Append("Do not break character or tone. ");
        sb.Append("Keep the text concise, under 350 characters. ");
        sb.Append("Do not repeat previous texts, and focus on advancing the story.");
        sb.Append("Narrative rules:\n- The environment, threats, and events are external to the party.\n- Player-controlled characters are already present and aware.\n- Never introduce a player character as something newly encountered.\n- Never have a player character beckon, warn, guide, or lure the party.\n- Focus on surroundings, omens, and hostile or uncanny non-player forces.\n");

        var generatedMessage = await entityGenerator.GenerateEntityAsync<GeneratedMessage>(
            sb.ToString(),
            TwistedTaleweaverContextHelper.NarrationContext,
            command.PreviousNarrativeTexts);

        return generatedMessage;
    }
}