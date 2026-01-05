using TwistedTaleweaver.Api.Expeditions.Entities;
using TwistedTaleweaver.Integration.OpenAi.Expeditions.Entities;

namespace TwistedTaleweaver.Integration.OpenAi.Expeditions.Mappers;

public static class ExpeditionContextMapper
{
    public static ExpeditionContext ToExpeditionContext(this Expedition source)
    {
        return new ExpeditionContext()
        {
            ExpeditionId = source.ExpeditionId,
            Name = source.Name,
            Description = source.Description,
            Difficulty = source.Difficulty,
        };
    }
}