namespace TwistedTaleweaver.Core.Kafka.Attributes;

/// <summary>
/// Attribute to mark a property as a partition key for Kafka events.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class PartitionKeyAttribute : Attribute
{
}