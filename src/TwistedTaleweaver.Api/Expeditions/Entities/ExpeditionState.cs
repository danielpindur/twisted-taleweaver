using TwistedTaleweaver.Api.Expeditions.Entities.Enums;

namespace TwistedTaleweaver.Api.Expeditions.Entities;

/// <summary>
/// Information regarding the state the expedition is in
/// </summary>
public class ExpeditionState
{
    /// <summary>
    /// Unique identifier of the expedition the state is for
    /// </summary>
    public Guid ExpeditionId { get; set; }
    
    /// <summary>
    /// <see cref="ExpeditionStatus"/>
    /// </summary>
    public ExpeditionStatus Status { get; set; }

    /// <summary>
    /// Unique identifier or the creature, whose turn it is to act.
    /// </summary>
    public Guid? ActiveCreatureId { get; set; }
}