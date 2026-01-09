using System.Text;
using TwistedTaleweaver.Integration.OpenAi.Creatures.Entities;

namespace TwistedTaleweaver.Integration.OpenAi.Messages.Extensions;

internal static class NarrativeStringBuilderExtensions
{
    public static StringBuilder AppendCharacters(this StringBuilder sb, IEnumerable<CharacterContext> characters)
    {
        foreach (var c in characters)
        {
            sb.AppendLine($"{c.Name}: {c.Description}. Status: {c.Status}");
        }
        return sb;
    }

    public static StringBuilder AppendMonsters(this StringBuilder sb, IEnumerable<MonsterContext> monsters)
    {
        foreach (var m in monsters)
        {
            sb.AppendLine($"{m.Name} ({m.CurrentHealth}/{m.MaxHealth} HP): {m.Description}");
        }
        return sb;
    }
}