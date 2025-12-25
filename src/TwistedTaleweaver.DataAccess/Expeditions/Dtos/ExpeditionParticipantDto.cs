using TwistedTaleweaver.DataAccess.Users.Dtos;

namespace TwistedTaleweaver.DataAccess.Expeditions.Dtos;

public class ExpeditionParticipantDto : UserDto
{
    public Guid ExpeditionId { get; set; }

    public Guid? CharacterId { get; set; }
}