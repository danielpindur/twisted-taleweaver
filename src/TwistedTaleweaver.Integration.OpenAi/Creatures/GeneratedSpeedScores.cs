using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Domain.Common;

namespace TwistedTaleweaver.Integration.OpenAi.Creatures;

/// <summary>
/// Represents how far a creature can move on its turn during combat
/// </summary>
public class GeneratedSpeedScores
{
    /// <summary>
    /// Default walking speed in feet per round
    /// </summary>
    [Range(0, 120)]
    public int Walking { get; init; }
    
    /// <summary>
    /// Optional. Represents the creature's speed while flying, if applicable
    /// </summary>
    [Range(0, 120)]
    public required Optional<int> Flying { get; init; }
    
    /// <summary>
    /// Optional. Represents the creature's speed while swimming, if applicable
    /// </summary>
    [Range(0, 120)]
    public required Optional<int> Swimming { get; init; }
    
    /// <summary>
    /// Optional. Represents the creature's speed while climbing, if applicable
    /// </summary>
    [Range(0, 120)]
    public required Optional<int> Climbing { get; init; }
   
    /// <summary>
    /// Optional. Represents the creature's speed while burrowing, if applicable
    /// </summary>
    [Range(0, 120)]
    public required Optional<int> Burrowing { get; init; }

    /// <summary>
    /// Optional. Represents the creature's speed while teleporting, if applicable
    /// </summary>
    [Range(0, 120)]
    public required Optional<int> Teleportation { get; init; }
}