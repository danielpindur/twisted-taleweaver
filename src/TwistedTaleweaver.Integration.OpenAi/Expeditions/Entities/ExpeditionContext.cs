using TwistedTaleweaver.Api.Expeditions.Entities.Enums;

namespace TwistedTaleweaver.Integration.OpenAi.Expeditions.Entities;

public class ExpeditionContext
{
    public Guid ExpeditionId { get; set; }
    
    public required string Name { get; set; }
    
    public required string Description { get; set; }
    
    public ExpeditionDifficulty Difficulty { get; set; }
}