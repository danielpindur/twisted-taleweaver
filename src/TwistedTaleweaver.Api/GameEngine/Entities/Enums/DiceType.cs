using System.ComponentModel;

namespace TwistedTaleweaver.Api.GameEngine.Entities.Enums;

/// <summary>
/// Type of dice to roll
/// </summary>
public enum DiceType
{
    [Description("d4")]
    FourSided = 4,

    [Description("d6")]
    SixSided = 6,

    [Description("d8")]
    EightSided = 8,

    [Description("d10")]
    TenSided = 10,

    [Description("d12")]
    TwelveSided = 12,
    
    [Description("d20")]
    TwentySided = 20,

    [Description("d100")]
    PercentileDice = 100
}