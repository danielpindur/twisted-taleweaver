using FluentValidation;

namespace TwistedTaleweaver.Gateway.Common.Errors;

public sealed class GatewayErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        if (error.Exception is ValidationException ex)
        {
            return FormatValidationError(ex);
        }

        return error;
    }

    private static IError FormatValidationError(ValidationException ex)
    {
        var errors = ex.Errors
            .Select(e =>
            {
                var fields = new Dictionary<string, object>
                {
                    ["message"] = e.ErrorMessage,
                    ["errorCode"] = e.ErrorCode
                };

                var propertyName = NormalizeField(e.PropertyName);
                if (propertyName != null)
                {
                    fields["property"] = propertyName;
                }

                return fields;
            })
            .ToList();

        return ErrorBuilder.New()
            .SetMessage("Validation failed")
            .SetCode(nameof(ErrorCode.VALIDATION_ERROR))
            .ClearLocations()
            .SetExtension("errors", errors)
            .Build();
    }

    private static string? NormalizeField(string propertyName)
        => string.IsNullOrWhiteSpace(propertyName)
            ? null
            : char.ToLowerInvariant(propertyName[0]) + propertyName[1..];
}
