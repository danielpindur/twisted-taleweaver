using System.Text.Json.Serialization;

namespace TwistedTaleweaver.Domain.Common;

/// <summary>
/// Generic class to get around limitations of OpenAI schema strict mode and nullable fields
/// </summary>
public class Optional<T>
{
    /// <summary>
    /// Actual value or default if not specified
    /// </summary>
    public required T ActualValue { get; set; }

    /// <summary>
    /// Whether the Value is specified or can be safely ignored
    /// </summary>
    public bool HasValue { get; set; } = false;

    /// <summary>
    /// Simple accessor to mimic the nullable implementation
    /// </summary>
    [JsonIgnore]
    public T? Value => HasValue ? ActualValue : default;
}