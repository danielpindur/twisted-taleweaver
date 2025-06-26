using System.Text.Json;
using Serilog.Core;
using Serilog.Events;

namespace TwistedTaleweaver.Core.Logging.Serilog;

/// <summary>
/// A custom Serilog destructuring policy for JsonElement.
/// This policy converts a JsonElement to a string representation of its raw JSON text.
/// </summary>
public class JsonElementDestructuringPolicy : IDestructuringPolicy
{
    public bool TryDestructure(object? value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
    {
        if (value is null)
        {
            result = null!;
            return false;
        }

        if (value is JsonElement jsonElement)
        {
            string jsonString = jsonElement.GetRawText();
            result = new ScalarValue(jsonString);
            return true;
        }

        result = null!;
        return false;
    }
}