namespace TwistedTaleweaver.Core.Kafka.Configuration;

/// <summary>
/// Configuration class for Kafka settings.
/// </summary>
public class KafkaConfiguration
{
    /// <summary>
    /// Kafka bootstrap servers.
    /// </summary>
    public required string BootstrapServers { get; set; }
    
    /// <summary>
    /// The group ID for the Kafka consumer.
    /// </summary>
    public string? GroupId { get; set; }
}
