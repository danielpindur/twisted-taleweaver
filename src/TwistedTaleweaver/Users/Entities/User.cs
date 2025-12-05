namespace TwistedTaleweaver.Users.Entities;

public class User
{
    public Guid UserId { get; set; }
    
    public required string ExternalUserId { get; set; }
}