using Tapper;

namespace TwistedTaleweaver.Hub.Expeditions.Contracts;

[TranspilationSource]
public class MessageRequest
{
    public required string Content { get; set; }
}