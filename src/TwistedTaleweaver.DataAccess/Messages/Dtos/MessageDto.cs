using TwistedTaleweaver.Domain.Messages.Enums;

namespace TwistedTaleweaver.DataAccess.Messages.Dtos;

public class MessageDto
{
    public Guid ExpeditionId { get; set; }
    
    public int ExpeditionMessageId { get; set; }
    
    public Guid? SenderCharacterId { get; set; }
    
    public MessageType Type { get; set; }
    
    public required string Content { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
}