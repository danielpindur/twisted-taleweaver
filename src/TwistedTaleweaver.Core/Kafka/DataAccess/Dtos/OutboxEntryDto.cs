using Confluent.Kafka;

namespace TwistedTaleweaver.Core.Kafka.DataAccess.Dtos;

internal class OutboxEntryDto
{
    public Guid OutboxEntryId { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public required string Topic { get; set; }
    
    public string? PartitionKey { get; set; }
    
    public string? Headers { get; set; }
    
    public required string Payload { get; set; }
    
    public DateTimeOffset? ProcessedAt { get; set; }

    public int RemainingAttempts { get; set; } = 3;
}