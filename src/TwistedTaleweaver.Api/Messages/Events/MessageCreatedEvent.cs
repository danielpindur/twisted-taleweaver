using TwistedTaleweaver.Api.Messages.Entities;
using TwistedTaleweaver.Api.Messages.Entities.Enums;
using TwistedTaleweaver.Core.Kafka.Attributes;

namespace TwistedTaleweaver.Api.Messages.Events;

public class MessageCreatedEvent
{
    public long MessageId { get; set; }

    [PartitionKey]
    public Guid ExpeditionId { get; set; }

    public required MessageSender? Sender { get; set; }

    public MessageType Type { get; set; }

    public required string Content { get; set; }

    public DateTimeOffset Timestamp { get; set; }
}