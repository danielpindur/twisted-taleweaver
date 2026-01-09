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
    /// Information about the expedition's completion status as a result of the action
    /// </summary>
    public required Optional<ExpeditionFinishInfo> ExpeditionFinishInfo { get; set; }
    
    /// <summary>
    /// Monsters that have been summoned or generated as a result of the action
    /// </summary>
    public required Optional<List<GeneratedMonster>> MonstersSummonedDueToActionOutcome { get; set; }
}

public class CreatureStateUpdate
{
    /// <summary>
    /// Identifier of the creature whose state is being updated
    /// </summary>
    [RegularExpression(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$")]
    public required string CreatureId { get; set; }
    
    /// <summary>
    /// Amount of damage taken by the creature as a result of the action
    /// </summary>
    public required Optional<int> DamageTaken { get; set; }
    
    /// <summary>
    /// Change in the creature's status (e.g., poisoned, stunned) as a result of the action
    /// </summary>
    public required Optional<CreatureStatus> CreatureStatusChange { get; set; }
}

/// <summary>
/// Information about whether the expedition has finished and its outcome
/// </summary>
public class ExpeditionFinishInfo
{
    /// <summary>
    /// Indicates whether the expedition has reached an end
    /// </summary>
    public bool ExpeditionHasReachedAnEnd { get; set; }
    
    /// <summary>
    /// Outcome of the expedition if it has finished
    /// </summary>
    public required ExpeditionOutcome OutcomeOfAFinishedExpedition { get; set; }
    
    /// <summary>
    /// Short lesson learned from the expedition if it has finished
    /// </summary>
    [MaxLength(500)]
    public required string ShortLessonLearnedFromTheExpedition { get; set; }
}