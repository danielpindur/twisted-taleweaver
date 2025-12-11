using TwistedTaleweaver.Domain.Expeditions;

namespace TwistedTaleweaver.DataAccess.Expeditions.Dtos;

public class ExpeditionSlimWithCountDto : ExpeditionSlim
{
    public int TotalCount { get; set; }
}