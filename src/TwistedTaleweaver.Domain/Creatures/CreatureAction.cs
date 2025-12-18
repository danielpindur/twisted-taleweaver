using System.ComponentModel.DataAnnotations;

namespace TwistedTaleweaver.Domain.Creatures;

/// <summary>
/// Action a creature can perform
/// </summary>
public class CreatureAction
{
    /// <summary>
    /// Name of the action
    /// </summary>
    [MaxLength(50)]
    public required string Name { get; init; }

    /// <summary>
    /// Description of the action's effect or purpose
    /// </summary>
    [MaxLength(255)]
    public required string Description { get; init; }

    /// <summary>
    /// Cost to perform the action, typically in terms of action economy (e.g., 1 action, 1 bonus action)
    /// </summary>
    [Range(1, 3)]
    public int Cost { get; init; }

    /// <summary>
    /// Optional. Recharge time for the action, if it can only be used after a certain period or condition 
    /// </summary>
    [Range(0, 6)]
    public int? Recharge { get; init; }
    
    /// <summary>
    /// Optional. Range of the action, typically in feet or a specific distance
    /// </summary>
    [Range(0, 120)]
    public int? Range { get; init; }
}