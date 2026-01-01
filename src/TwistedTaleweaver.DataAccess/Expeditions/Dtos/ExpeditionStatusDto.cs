using TwistedTaleweaver.Api.Expeditions.Entities.Enums;

namespace TwistedTaleweaver.DataAccess.Expeditions.Dtos;

public class ExpeditionStatusDto
{
    public Guid ExpeditionId { get; set; }
    
    public ExpeditionStatus Status { get; set; }
}