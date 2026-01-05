using TwistedTaleweaver.Api.Creatures.Entities.Enums;
using TwistedTaleweaver.Api.GameEngine.Entities.Enums;
using TwistedTaleweaver.Integration.OpenAi.Messages.Entities.Enums;

namespace TwistedTaleweaver.GameEngine.Entities;

public class RequiredCheck
{
    public AttributeType Attribute { get; init; }

    public CheckDifficulty Difficulty { get; init; }
}