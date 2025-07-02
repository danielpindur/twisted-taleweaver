namespace TwistedTaleweaver.DataAccess.Characters.Entities;

public class Character
{
    public Guid CharacterId { get; set; }
    
    public required string ExternalUserId { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid BroadcasterUserId { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset UpdatedAt { get; set; }
}