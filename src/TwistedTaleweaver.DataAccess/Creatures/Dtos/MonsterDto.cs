namespace TwistedTaleweaver.DataAccess.Creatures.Dtos;

public class MonsterDto : CombatCreatureDto
{
    public Guid ExpeditionId { get; set; }
    
    public int ChallengeRating { get; set; }
}