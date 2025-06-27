namespace TwistedTaleweaver.Core.Logging.Entities;

/// <summary>
/// Represents a single HTTP log entry.
/// </summary>
public sealed class HttpLogEntry
{
    /// <summary>
    /// The timestamp when the log entry was created.
    /// </summary>
    public DateTimeOffset Timestamp { get; init; }

    /// <summary>
    /// The request information associated with the log entry.
    /// </summary>
    public required RequestInfo Request { get; init; }

    /// <summary>
    /// The response information associated with the log entry, if available.
    /// </summary>
    public ResponseInfo? Response { get; set; }

    /// <summary>
    /// The exception information associated with the log entry, if an exception occurred.
    /// </summary>
    public ExceptionInfo? Exception { get; set; }

    /// <summary>
    /// The duration in milliseconds that the request took to process.
    /// This is set if the request was successful or if an exception occurred.
    /// </summary>
    public double? DurationMs { get; set; }
}