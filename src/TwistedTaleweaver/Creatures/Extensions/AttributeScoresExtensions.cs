using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Api.Creatures.Entities.Enums;

namespace TwistedTaleweaver.Creatures.Extensions;

internal static class AttributeScoresExtensions
{
    public static int GetAttributeBonus(this AttributeScores attributeScores, AttributeType attributeType)
    {
        return (attributeScores.GetAttributeScore(attributeType) - 10) / 2;
    }
    
    private static int GetAttributeScore(this AttributeScores attributeScores, AttributeType attributeType)
    {
        return attributeType switch
        {
            AttributeType.Strength => attributeScores.Strength,
            AttributeType.Dexterity => attributeScores.Dexterity,
            AttributeType.Constitution => attributeScores.Constitution,
            AttributeType.Intelligence => attributeScores.Intelligence,
            AttributeType.Wisdom => attributeScores.Wisdom,
            AttributeType.Charisma => attributeScores.Charisma,
            _ => throw new ArgumentOutOfRangeException(nameof(attributeType), attributeType, null)
        };
    }
}