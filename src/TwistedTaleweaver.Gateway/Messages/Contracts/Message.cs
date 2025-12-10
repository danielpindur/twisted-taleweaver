using Tapper;
using TwistedTaleweaver.Domain.Users;

namespace TwistedTaleweaver.Gateway.Messages.Contracts;

[TranspilationSource]
public class Message
{
    public Guid MessageId { get; set; }
    
    public Guid ExpeditionId { get; set; }
    
    public required User User { get; set; }
    
    public required string Text { get; set; }
}