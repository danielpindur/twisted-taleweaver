using FluentValidation;
using FluentValidation.Results;

namespace TwistedTaleweaver.Core.Validation;

public static class ValidationResultExtensions
{
    public static void ThrowIfInvalid(this ValidationResult? validationResult)
    {
        if (validationResult == null)
        {
            return;
        }

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
    }
}