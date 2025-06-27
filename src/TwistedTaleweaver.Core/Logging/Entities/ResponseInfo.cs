namespace TwistedTaleweaver.Core.Logging.Entities;

/// <summary>
/// Represents information about a HTTP response.
/// </summary>
public sealed class ResponseInfo
{
    /// <summary>
    /// The HTTP status code of the response (e.g., 200, 404).
    /// </summary>
    public int StatusCode { get; init; }

    /// <summary>
    /// The reason phrase associated with the status code (e.g., "OK", "Not Found").
    /// </summary>
    public string? ReasonPhrase { get; init; }

    /// <summary>
    /// Headers associated with the response.
    /// </summary>
    public Dictionary<string, string> Headers { get; init; } = new();

    /// <summary>
    /// The body of the response, if available.
    /// </summary>
    public string? Body { get; init; }

    /// <summary>
    /// The length of the content in the response body, if available.
    /// </summary>
    public int? ContentLength { get; init; }
}