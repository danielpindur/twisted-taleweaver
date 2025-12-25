using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Domain.Creatures;
using TwistedTaleweaver.Domain.Expeditions.Enums;
using TwistedTaleweaver.Domain.Users;

namespace TwistedTaleweaver.Domain.Expeditions;

/// <summary>
/// Information regarding the state the expedition is in
/// </summary>
public class ExpeditionState
{
    /// <summary>
    /// <see cref="ExpeditionStatus"/>
    /// </summary>
    [Required]
    public ExpeditionStatus Status { get; set; }

    /// <summary>
    /// The user whose turn it is to act
    /// </summary>
    public User? ActiveUser { get; set; }

    /// <summary>
    /// Current states of the characters participating in the expedition
    /// </summary>
    [Required]
    public required List<CombatCreatureState> Participants { get; set; }
}