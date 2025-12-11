namespace TwistedTaleweaver.DataAccess.Users.Dtos;

public class UserDto
{
    public Guid UserId { get; set; }
    
    public required string ExternalUserId { get; set; }
}