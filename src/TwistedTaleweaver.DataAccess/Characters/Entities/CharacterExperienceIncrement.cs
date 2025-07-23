namespace TwistedTaleweaver.DataAccess.Characters.Entities;

public class CharacterExperienceIncrement
{
    public long CharacterExperienceIncrementId { get; set; }
    
    public Guid CharacterId { get; set; }
    
    public int Amount { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public Guid? ExpeditionId { get; set; }
}