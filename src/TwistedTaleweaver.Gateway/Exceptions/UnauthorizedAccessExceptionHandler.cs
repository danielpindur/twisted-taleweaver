using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TwistedTaleweaver.Gateway.Exceptions;

internal sealed class UnauthorizedAccessExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<UnauthorizedAccessExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not UnauthorizedAccessException unauthorizedException)
        {
            return false;
        }

        logger.LogWarning(unauthorizedException, "Unauthorized access attempt");

        httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;

        var context = new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = unauthorizedException,
            ProblemDetails = new ProblemDetails
            {
                Title = "Forbidden",
                Detail = unauthorizedException.Message,
                Status = StatusCodes.Status403Forbidden
            }
        };

        return await problemDetailsService.TryWriteAsync(context);
    }
}
