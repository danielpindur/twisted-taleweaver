using TwistedTaleweaver.Api.Expeditions.Entities;

namespace TwistedTaleweaver.DataAccess.Expeditions.Dtos;

public class ExpeditionWithCount : Expedition
{
    public int TotalCount { get; set; }
}