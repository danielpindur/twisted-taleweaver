using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Api.Creatures.Entities.Enums;
using TwistedTaleweaver.Api.GameEngine.Entities.Enums;

namespace TwistedTaleweaver.Integration.OpenAi.Messages.Entities;

/// <summary>
/// Represents a check that needs to be performed to determine the success of an action
/// </summary>
public class RequiredCheck
{
    /// <summary>
    /// The attribute that the check is based on
    /// </summary>
    public AttributeType Attribute { get; init; }

    /// <summary>
    /// The difficulty level of the check
    /// </summary>
    public CheckDifficulty Difficulty { get; init; }
    
    /// <summary>
    /// The target value that needs to be beaten with the roll
    /// </summary>
    public int ValueToBeatWithRoll { get; init; }
    
    /// <summary>
    /// The type of dice to roll for the check
    /// </summary>
    public DiceType DiceToRoll { get; init; }
    
    /// <summary>
    /// The number of dice to roll for the check
    /// </summary>
    public int NumberOfDicesToRoll { get; init; }

    /// <summary>
    /// Justification for why the check is required
    /// </summary>
    [MaxLength(255)]
    [Required]
    public required string Justification { get; init; }
}