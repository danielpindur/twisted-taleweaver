namespace TwistedTaleweaver.Core.Logging.Entities;

/// <summary>
/// Represents information about an exception that occurred during HTTP request processing.
/// </summary>
public sealed class ExceptionInfo
{
    /// <summary>
    /// The message describing the exception.
    /// </summary>
    public required string Message { get; set; }

    /// <summary>
    /// The stack trace of the exception, if available.
    /// </summary>
    public string? StackTrace { get; set; }

    /// <summary>
    /// The duration in milliseconds that the request took before the exception occurred.
    /// </summary>
    public double DurationMs { get; set; }
}