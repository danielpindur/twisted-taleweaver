using Tapper;

namespace TwistedTaleweaver.Hub.Expeditions.Contracts;

[TranspilationSource]
public class MessageReceivedEvent
{
    public long MessageId { get; set; }
    
    public Guid SenderCreatureId { get; set; }
    
    public string? SenderType { get; set; }
    
    public required string Type { get; set; }
    
    public required string Content { get; set; }
    
    public DateTimeOffset Timestamp { get; set;}
}