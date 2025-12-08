using Tapper;

namespace TwistedTaleweaver.Gateway.Messages.Contracts;

[TranspilationSource]
public class MessageRequest
{
    public Guid ExpeditionId { get; set; }
    
    public required string UserId { get; set; }
    
    public required string Text { get; set; }
}