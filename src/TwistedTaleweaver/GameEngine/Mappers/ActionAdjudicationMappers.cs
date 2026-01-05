using TwistedTaleweaver.GameEngine.Entities;
using TwistedTaleweaver.Integration.OpenAi.Messages.Entities;
using GeneratedRequiredCheck = TwistedTaleweaver.Integration.OpenAi.Messages.Entities.RequiredCheck;
using RequiredCheck = TwistedTaleweaver.Api.GameEngine.Entities.RequiredCheck;

namespace TwistedTaleweaver.GameEngine.Mappers;

internal static class ActionAdjudicationMappers
{
    public static ActionAdjudication ToActionAdjudication(this GeneratedActionAdjudication source, Guid creatureId)
    {
        return new ActionAdjudication()
        {
            CreatureId = creatureId,
            RequiredChecks = source.RequiredChecks.ToRequiredChecks()
        };
    }

    private static List<RequiredCheck> ToRequiredChecks(this List<GeneratedRequiredCheck> source)
    {
        return source.Select(ToRequiredCheck).ToList();
    }

    private static RequiredCheck ToRequiredCheck(this GeneratedRequiredCheck source)
    {
        return new RequiredCheck()
        {
            Attribute = source.Attribute,
            ValueToBeat = source.ValueToBeatWithRoll,
            DiceType = source.DiceToRoll,
            DiceCount = source.NumberOfDicesToRoll
        };
    }
}