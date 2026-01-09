using System.Text;
using System.Text.Json;
using TwistedTaleweaver.Integration.OpenAi.Creatures.Entities;
using TwistedTaleweaver.Integration.OpenAi.Expeditions.Entities;
using TwistedTaleweaver.Integration.OpenAi.Generators;
using TwistedTaleweaver.Integration.OpenAi.Messages.Entities;

namespace TwistedTaleweaver.Integration.OpenAi.Messages;

public class CharacterActionAdjudicationGenerationCommand
{
    public required string ActionText { get; set; }
    
    public required CharacterContext CharacterPerformingAction { get; set; }
    
    public required ExpeditionContext Expedition { get; set; }
    
    public required IEnumerable<string> PreviousNarrativeTexts { get; set; }
    
    public required List<CharacterContext> OtherCharacters { get; set; }
    
    public required List<MonsterContext> AliveMonsters { get; set; }
}

public class MonsterActionAdjudicationGenerationCommand
{
    public required MonsterContext MonsterPerformingAction { get; set; }
    
    public required ExpeditionContext Expedition { get; set; }
    
    public required IEnumerable<string> PreviousNarrativeTexts { get; set; }
    
    public required List<MonsterContext> OtherAliveMonsters { get; set; }
    
    public required List<CharacterContext> PlayerCharactersInExpedition { get; set; }
}

public interface IActionAdjudicationGenerationAdapter
{
    Task<GeneratedCharacterActionAdjudication> GenerateAsync(CharacterActionAdjudicationGenerationCommand command);
    
    Task<GeneratedMonsterActionAdjudication> GenerateAsync(MonsterActionAdjudicationGenerationCommand command);
}

internal class ActionAdjudicationGenerationAdapter(IEntityGenerator entityGenerator) : IActionAdjudicationGenerationAdapter
{
    public async Task<GeneratedCharacterActionAdjudication> GenerateAsync(CharacterActionAdjudicationGenerationCommand command)
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
        
        sb.Append("Monsters currently alive, that the expeditioners are fighting against:\n");
        sb.Append(JsonSerializer.Serialize(command.AliveMonsters));
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
                  - ShortActionDescription should be 200 characters long.
                  """);

        return await entityGenerator.GenerateEntityAsync<GeneratedCharacterActionAdjudication>(
            sb.ToString(),
            TwistedTaleweaverContextHelper.ActionAdjudicationContext);
    }
    
    public async Task<GeneratedMonsterActionAdjudication> GenerateAsync(MonsterActionAdjudicationGenerationCommand command)
    {
        var sb = new StringBuilder();

        sb.Append("You are adjudicating a monster action in a dark fantasy roleplaying game.\n");
        sb.Append("You must FIRST decide what action the monster attempts.\n\n");

        sb.Append("Monster performing the action:\n");
        sb.Append(JsonSerializer.Serialize(command.MonsterPerformingAction));
        sb.Append("\n\n");

        sb.Append("Other monsters still alive:\n");
        sb.Append(JsonSerializer.Serialize(command.OtherAliveMonsters));
        sb.Append("\n\n");

        sb.Append("Player characters present in the expedition:\n");
        sb.Append(JsonSerializer.Serialize(command.PlayerCharactersInExpedition));
        sb.Append("\n\n");

        sb.Append("Expedition context:\n");
        sb.Append(JsonSerializer.Serialize(command.Expedition));
        sb.Append("\n\n");

        sb.Append("Recent narrative context:\n");
        foreach (var narrativeText in command.PreviousNarrativeTexts)
        {
            sb.Append($"- {narrativeText}\n");
        }

        sb.Append("""
                  
                  Rules:
                  - FIRST determine the monster's intended action.
                  - The action must be consistent with the monster's nature, intelligence, traits, and current situation.
                  - Monsters do not act optimally unless they are intelligent and tactical.
                  - Do NOT narrate outcomes.
                  - Determine whether the chosen action is physically and logically possible.
                  - If possible, determine which attribute checks are required.
                  - Attribute checks must use ONLY the provided attributes.
                  - Traits may modify or justify checks but do not auto-succeed unless explicitly stated.
                  - Be strict but fair.
                  - Return structured data only.
                  - ShortActionDescription should be 200 characters long.
                  """);

        return await entityGenerator.GenerateEntityAsync<GeneratedMonsterActionAdjudication>(
            sb.ToString(),
            TwistedTaleweaverContextHelper.ActionAdjudicationContext);
    }
}