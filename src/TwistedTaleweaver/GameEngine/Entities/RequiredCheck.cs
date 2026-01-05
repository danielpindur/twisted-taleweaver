using TwistedTaleweaver.Api.Creatures.Entities.Enums;
using TwistedTaleweaver.Api.GameEngine.Entities.Enums;

namespace TwistedTaleweaver.GameEngine.Entities;

public class RequiredCheck
{
    public AttributeType Attribute { get; init; }

    public int ValueToBeat { get; init; }
    
    public DiceType DiceType { get; init; }
    
    public int DiceCount { get; init; }
}