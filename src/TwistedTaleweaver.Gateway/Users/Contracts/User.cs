namespace TwistedTaleweaver.Gateway.Users.Contracts;

/// <summary>
/// Represents a user within the system
/// </summary>
public class User
{
    /// <summary>
    /// The unique identifier of the user
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// The display name of the user
    /// </summary>
    public required string Name { get; set; }
}