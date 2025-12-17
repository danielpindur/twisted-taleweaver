using Microsoft.AspNetCore.Mvc;

namespace TwistedTaleweaver.Gateway.Exceptions.Contracts;

public class ValidationProblemDetails : ProblemDetails
{
    public IDictionary<string, string[]> Errors { get; init; } =
        new Dictionary<string, string[]>();
}