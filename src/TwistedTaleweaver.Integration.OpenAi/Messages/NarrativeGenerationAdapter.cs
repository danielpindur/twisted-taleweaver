using System.Text;
using TwistedTaleweaver.Integration.OpenAi.Creatures.Entities;
using TwistedTaleweaver.Integration.OpenAi.Expeditions.Entities;
using TwistedTaleweaver.Integration.OpenAi.Generators;
using TwistedTaleweaver.Integration.OpenAi.Messages.Entities;
using TwistedTaleweaver.Integration.OpenAi.Messages.Extensions;

namespace TwistedTaleweaver.Integration.OpenAi.Messages;

public class NarrativeGenerationCommand
{
    public required ExpeditionContext Expedition { get; set; }
    
    public bool IsFirstNarrativeText { get; set; }
    
    public required IEnumerable<string> PreviousNarrativeTexts { get; set; }
    
    public required List<CharacterContext> Characters { get; set; }
    
    public required List<MonsterContext> AliveMonsters { get; set; }
    
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

        // Instruction for AI
        sb.AppendLine("You are Twisted Taleweaver, narrating only the consequences of recent actions.");
        sb.AppendLine("Focus on the environment and external threats. Do not summarize the expedition or repeat prior narration.");

        // Include expedition context only if first message
        if (command.IsFirstNarrativeText)
        {
            sb.AppendLine($"Expedition Name: {command.Expedition.Name}");
            sb.AppendLine($"Theme: {command.Expedition.Description}");
            sb.AppendLine($"Difficulty: {command.Expedition.Difficulty}");
            sb.AppendLine();
        }

        // Present characters and monsters briefly
        sb.AppendLine("Characters currently present:");
        sb.AppendCharacters(command.Characters);
        sb.AppendLine();
        sb.AppendLine("Hostile forces currently active:");
        sb.AppendMonsters(command.AliveMonsters);
        sb.AppendLine();

        // Actions since last narration
        sb.AppendLine("Recent actions and their mechanical outcomes (must be transformed into cruel consequences):");
        foreach (var action in command.ActionMessagesSinceLastNarration)
        {
            sb.AppendLine($"- {action}");
        }
        sb.AppendLine();

        // Previous narrative texts to avoid repetition
        if (!command.IsFirstNarrativeText)
        {
            sb.AppendLine("Previous narrative texts (do not repeat or summarize):");
            foreach (var previousNarrative in command.PreviousNarrativeTexts)
            {
                sb.AppendLine($"- {previousNarrative}");
            }
            sb.AppendLine();
        }

        // Explicit narrative instructions
        sb.AppendLine("Narrative instructions:");
        sb.AppendLine("- Write only 2–4 sentences.");
        sb.AppendLine("- Do not ask questions or offer hope.");
        sb.AppendLine("- Show grotesque, cruel, or absurd consequences of the actions.");
        sb.AppendLine("- Base narrative strictly on recent actions and their outcomes.");
        sb.AppendLine("- Do not summarize battles or repeat prior narration.");
        sb.AppendLine("- Maximum 350 characters.");
        sb.AppendLine("- Mock suffering and escalate dread.");

        if (!command.IsFirstNarrativeText)
        {
            sb.AppendLine("- Do not repeat expedition introduction.");
        }

        var generatedMessage = await entityGenerator.GenerateEntityAsync<GeneratedMessage>(
            sb.ToString(),
            TwistedTaleweaverContextHelper.NarrationContext,
            RandomnessSettingsHelper.NarrativeSettings);

        return generatedMessage;
    }
}