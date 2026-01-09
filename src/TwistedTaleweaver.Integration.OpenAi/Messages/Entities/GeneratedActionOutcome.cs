using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Api.Creatures.Entities.Enums;
using TwistedTaleweaver.Api.Expeditions.Entities.Enums;
using TwistedTaleweaver.Domain.Common;
using TwistedTaleweaver.Integration.OpenAi.Creatures.Entities;

namespace TwistedTaleweaver.Integration.OpenAi.Messages.Entities;

/// <summary>
/// Outcome of an action taken by player-controlled characters or monsters within an expedition 
/// </summary>
public class GeneratedActionOutcome
{
    /// <summary>
    /// Updates to the state of player-controlled characters as a result of the action
    /// </summary>
    public required Optional<List<CreatureStateUpdate>> PlayerControlledCharacterUpdates { get; set; }
    
    /// <summary>
    /// Updates to the state of monsters that player-controlled characters are fighting against as a result of the action
    /// </summary>
    public required Optional<List<CreatureStateUpdate>> UpdatesForMonstersCharactersAreFightingAgainst { get; set; }
    
    /// <summary>
    /// Short description summarizing the outcome of the action
    /// </summary>
    [MaxLength(250)]
    public required string ActionOutcomeShortDescription { get; set; }
    
    /// <summary>
    /// Updates to the state of the expedition as a result of the action
    /// </summary>
    public required Optional<ExpeditionStateUpdate> ExpeditionStateUpdate { get; set; }
    
    /// <summary>
    /// Monsters that have been summoned or generated as a result of the action
    /// </summary>
    public required Optional<List<GeneratedMonster>> MonstersSummonedDueToActionOutcome { get; set; }
}

public class CreatureStateUpdate
{
    [RegularExpression(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$")]
    public required string CreatureId { get; set; }
    
    public required Optional<int> HealthChange { get; set; }
    
    public required Optional<CreatureStatus> CreatureStatusChange { get; set; }
}

public class ExpeditionStateUpdate
{
    public bool ExpeditionHasReachedAnEnd { get; set; }
    
    public required Optional<ExpeditionOutcome> OutcomeOfAFinishedExpedition { get; set; }
}