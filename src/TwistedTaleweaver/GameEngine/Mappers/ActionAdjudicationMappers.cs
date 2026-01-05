using TwistedTaleweaver.GameEngine.Entities;
using TwistedTaleweaver.Integration.OpenAi.Messages.Entities;
using GeneratedRequiredCheck = TwistedTaleweaver.Integration.OpenAi.Messages.Entities.RequiredCheck;
using RequiredCheck = TwistedTaleweaver.GameEngine.Entities.RequiredCheck;

namespace TwistedTaleweaver.GameEngine.Mappers;

internal static class ActionAdjudicationMappers
{
    public static ActionAdjudication ToActionAdjudication(this GeneratedActionAdjudication source)
    {
        return new ActionAdjudication()
        {
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
            Difficulty = source.Difficulty
        };
    }
}