namespace TwistedTaleweaver.DataAccess.Streams.Entities;

public class Stream
{
    public Guid StreamId { get; set; }
    
    public Guid BroadcasterId { get; set; }
    
    public required string ExternalStreamId { get; set; }
    
    public DateTimeOffset StartedAt { get; set; }
    
    public DateTimeOffset? EndedAt { get; set; }
}