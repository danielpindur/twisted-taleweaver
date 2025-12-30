using System.ComponentModel.DataAnnotations;
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
    /// Unique identifier or the user whose turn it is to act
    /// </summary>
    public Guid? ActiveUserId { get; set; }

    /// <summary>
    /// Current states of the characters participating in the expedition
    /// </summary>
    //[Required]
    //public required List<CombatCreatureState> Participants { get; set; }
}