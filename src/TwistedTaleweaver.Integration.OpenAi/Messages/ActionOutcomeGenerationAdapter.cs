using System.Text;
using System.Text.Json;
using TwistedTaleweaver.Api.GameEngine.Entities;
using TwistedTaleweaver.Integration.OpenAi.Creatures.Entities;
using TwistedTaleweaver.Integration.OpenAi.Expeditions.Entities;
using TwistedTaleweaver.Integration.OpenAi.Generators;
using TwistedTaleweaver.Integration.OpenAi.Messages.Entities;

namespace TwistedTaleweaver.Integration.OpenAi.Messages;

public class CharacterActionOutcomeGenerationCommand
{
    public required CharacterContext CharacterPerformingAction { get; set; }
    
    public required ExpeditionContext Expedition { get; set; }
    
    public required IEnumerable<string> PreviousNarrativeTexts { get; set; }
    
    public required List<CharacterContext> OtherCharacters { get; set; }

    public required List<MonsterContext> AliveMonsters { get; set; }
    
    public required string ActionAdjudicationMessage { get; set; }
    
    public required List<CheckResult> CheckResults { get; set; }
}

public class MonsterActionOutcomeGenerationCommand
{
    public required MonsterContext MonsterPerformingAction { get; set; }
    
    public required ExpeditionContext Expedition { get; set; }
    
    public required IEnumerable<string> PreviousNarrativeTexts { get; set; }
    
    public required List<MonsterContext> OtherAliveMonsters { get; set; }

    public required List<CharacterContext> PlayerCharactersInExpedition { get; set; }
    
    public required string ActionAdjudicationMessage { get; set; }
    
    public required List<CheckResult> CheckResults { get; set; }
}

public interface IActionOutcomeGenerationAdapter
{
    Task<GeneratedActionOutcome> GenerateAsync(CharacterActionOutcomeGenerationCommand command);
    
    Task<GeneratedActionOutcome> GenerateAsync(MonsterActionOutcomeGenerationCommand command);
}


internal class ActionOutcomeGenerationAdapter(IEntityGenerator entityGenerator) : IActionOutcomeGenerationAdapter
{
    public Task<GeneratedActionOutcome> GenerateAsync(CharacterActionOutcomeGenerationCommand command)
    {
        var sb = new StringBuilder();
        
        sb.Append("Action adjudication summary:\n");
        sb.Append(command.ActionAdjudicationMessage);
        sb.Append("\n");

        sb.Append("User controlled character performing the action:\n");
        sb.Append(JsonSerializer.Serialize(command.CharacterPerformingAction));
        sb.Append("\n");

        sb.Append("Other characters present:\n");
        sb.Append(JsonSerializer.Serialize(command.OtherCharacters));
        sb.Append("\n");
        
        sb.Append("Monsters currently alive and already hostile:\n");
        sb.Append(JsonSerializer.Serialize(command.AliveMonsters));
        sb.Append("\n");

        sb.Append("Expedition context:\n");
        sb.Append(JsonSerializer.Serialize(command.Expedition));
        sb.Append("\n");

        sb.Append("Recent narrative context:\n");
        foreach (var narrative in command.PreviousNarrativeTexts)
        {
            sb.Append($"- {narrative}\n");
        }

        sb.Append("\nCheck results:\n");
        sb.Append(JsonSerializer.Serialize(command.CheckResults));
        sb.Append("\n");

        sb.Append("""
                  Resolution rules:

                  General:
                  - Use ONLY the provided check results.
                  - Effects must be reasonable, grounded, and proportional.
                  - Do NOT introduce new entities unless explicitly discovered by the action.
                  - Do NOT apply effects without a clear causal link to the action.
                  - Health changes must be small unless explicitly justified.
                  - Status changes must use only known CreatureStatus values.
                  - If no meaningful effect occurs, return empty updates.
                  - The short description must be factual, concise, and under 200 characters.

                  Player character update rules (CRITICAL):
                  - PlayerControlledCharacterUpdates MUST only include characters whose state actually changes.
                  - A valid state change is defined as:
                    - Health value differs from the pre-action state, OR
                    - One or more CreatureStatus values are added or removed.
                  - If a character's health and statuses remain exactly the same, DO NOT include an update entry.
                  - Narrative-only effects (emotion, positioning, flavor, tension) do NOT count as state changes.
                  - NEVER emit no-op updates.
                  
                  Outcome classification (CRITICAL):
                  - MonstersSummonedDueToActionOutcome MUST include:
                    - Any newly discovered monster that becomes aggressive.
                    - Any previously neutral or passive monster that becomes hostile due to the action.
                  - A monster becoming hostile is considered a "summoning" event, even if it already existed.

                  - UpdatesForMonstersCharactersAreFightingAgainst MUST include:
                    - State, status, or health changes to monsters that were already hostile BEFORE the action.
                    - Monsters listed here must NOT appear in MonstersSummonedDueToActionOutcome.

                  - NEVER place the same monster in both collections.
                  - If a monster only reacts narratively without hostility, do NOT include it in either collection.
                  """);

        return entityGenerator.GenerateEntityAsync<GeneratedActionOutcome>(
            sb.ToString(),
            TwistedTaleweaverContextHelper.ActionOutcomeContext);
    }

    public Task<GeneratedActionOutcome> GenerateAsync(MonsterActionOutcomeGenerationCommand command)
    {
        var sb = new StringBuilder();
        
        sb.Append("Action adjudication summary:\n");
        sb.Append(command.ActionAdjudicationMessage);
        sb.Append("\n");

        sb.Append("Monster performing the action:\n");
        sb.Append(JsonSerializer.Serialize(command.MonsterPerformingAction));
        sb.Append("\n");

        sb.Append("Other mnsters currently alive and already hostile:\n");
        sb.Append(JsonSerializer.Serialize(command.OtherAliveMonsters));
        sb.Append("\n");

        sb.Append("Player controlled characters participating in the expedition:\n");
        sb.Append(JsonSerializer.Serialize(command.PlayerCharactersInExpedition));
        sb.Append("\n");

        sb.Append("Expedition context:\n");
        sb.Append(JsonSerializer.Serialize(command.Expedition));
        sb.Append("\n");

        sb.Append("Recent narrative context:\n");
        foreach (var narrative in command.PreviousNarrativeTexts)
        {
            sb.Append($"- {narrative}\n");
        }

        sb.Append("\nCheck results:\n");
        sb.Append(JsonSerializer.Serialize(command.CheckResults));
        sb.Append("\n");

        sb.Append("""
                  Resolution rules:

                  General:
                  - Use ONLY the provided check results.
                  - Effects must be reasonable, grounded, and proportional.
                  - Do NOT introduce new entities unless explicitly discovered by the action.
                  - Do NOT apply effects without a clear causal link to the action.
                  - Health changes must be small unless explicitly justified.
                  - Status changes must use only known CreatureStatus values.
                  - If no meaningful effect occurs, return empty updates.
                  - The short description must be factual, concise, and under 200 characters.

                  Player character update rules (CRITICAL):
                  - PlayerControlledCharacterUpdates MUST only include characters whose state actually changes.
                  - A valid state change is defined as:
                    - Health value differs from the pre-action state, OR
                    - One or more CreatureStatus values are added or removed.
                  - If a character's health and statuses remain exactly the same, DO NOT include an update entry.
                  - Narrative-only effects (emotion, positioning, flavor, tension) do NOT count as state changes.
                  - NEVER emit no-op updates.
                  
                  Outcome classification (CRITICAL):
                  - MonstersSummonedDueToActionOutcome MUST include:
                    - Any newly discovered monster that becomes aggressive.
                    - Any previously neutral or passive monster that becomes hostile due to the action.
                  - A monster becoming hostile is considered a "summoning" event, even if it already existed.

                  - UpdatesForMonstersCharactersAreFightingAgainst MUST include:
                    - State, status, or health changes to monsters that were already hostile BEFORE the action.
                    - Monsters listed here must NOT appear in MonstersSummonedDueToActionOutcome.

                  - NEVER place the same monster in both collections.
                  - If a monster only reacts narratively without hostility, do NOT include it in either collection.
                  """);

        return entityGenerator.GenerateEntityAsync<GeneratedActionOutcome>(
            sb.ToString(),
            TwistedTaleweaverContextHelper.ActionOutcomeContext);
    }
}
