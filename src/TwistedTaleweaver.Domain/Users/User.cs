using System.ComponentModel.DataAnnotations;

namespace TwistedTaleweaver.Domain.Users;

/// <summary>
/// Represents a user within the system
/// </summary>
public class User
{
    /// <summary>
    /// The unique identifier of the user
    /// </summary>
    [Required]
    public Guid UserId { get; set; }
    
    /// <summary>
    /// The external identifier of the user (e.g., from an authentication provider)
    /// </summary>    
    [Required]
    public required string ExternalUserId { get; set; }
    
    /// <summary>
    /// The display name of the user
    /// </summary>
    [Required]
    public required string Name { get; set; }
}