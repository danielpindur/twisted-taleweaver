using TwistedTaleweaver.Integration.OpenAi.Creatures.Entities;

namespace TwistedTaleweaver.Creatures.Helpers;

internal static class MonstersHelper
{
    public static int GetProficiencyBonus(this MonsterContext monsterContext)
    {
        return monsterContext.ChallengeRating switch
        {
            <= 4 => 2,
            <= 8 => 3,
            <= 12 => 4,
            <= 16 => 5,
            <= 20 => 6,
            <= 24 => 7,
            <= 28 => 8,
            _ => 9
        };
    }
}