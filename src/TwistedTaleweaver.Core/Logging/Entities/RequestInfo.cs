namespace TwistedTaleweaver.Core.Logging.Entities;

/// <summary>
/// Represents information about a HTTP request.
/// </summary>
public sealed class RequestInfo
{
    /// <summary>
    /// The HTTP method used for the request (e.g., GET, POST).
    /// </summary>
    public required string Method { get; init; }

    /// <summary>
    /// The URL of the request.
    /// </summary>
    public required string Url { get; init; }

    /// <summary>
    /// The HTTP version used for the request (e.g., HTTP/1.1).
    /// </summary>
    public required string HttpVersion { get; init; }

    /// <summary>
    /// A collection of headers associated with the request.
    /// </summary>
    public Dictionary<string, string> Headers { get; init; } = new();

    /// <summary>
    /// The body of the request, if available.
    /// </summary>
    public string? Body { get; init; }

    /// <summary>
    /// The length of the content in the request body, if available.
    /// </summary>
    public int? ContentLength { get; init; }
}