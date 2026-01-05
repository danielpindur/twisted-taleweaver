using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Api.Creatures.Entities.Enums;
using TwistedTaleweaver.Domain.Common;

namespace TwistedTaleweaver.Integration.OpenAi.Messages.Entities;

public class GeneratedActionOutcome
{
    public required Optional<List<CreatureStateUpdate>> PlayerControlledCharacterUpdates { get; set; }
    
    // TODO: Monster updates
    
    [MaxLength(250)]
    public required string ActionOutcomeShortDescription { get; set; }
}

public class CreatureStateUpdate
{
    [RegularExpression(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$")]
    public required string CreatureId { get; set; }
    
    public required Optional<int> HealthChange { get; set; }
    
    public required Optional<CreatureStatus> CreatureStatusChange { get; set; }
}