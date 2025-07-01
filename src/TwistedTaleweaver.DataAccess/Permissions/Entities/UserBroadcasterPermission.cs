using TwistedTaleweaver.DataAccess.Permissions.Entities.Enums;

namespace TwistedTaleweaver.DataAccess.Permissions.Entities;

public class UserBroadcasterPermission
{
    public Guid PermissionId { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid BroadcasterUserId { get; set; }
    
    public PermissionType PermissionType { get; set; }
    
    public bool IsActive { get; set; }
    
    public DateTimeOffset GrantedAt { get; set; }
    
    public Guid GrantedByUserId { get; set; }
    
    public DateTimeOffset? RevokedAt { get; set; }
    
    public Guid? RevokedByUserId { get; set; }
}