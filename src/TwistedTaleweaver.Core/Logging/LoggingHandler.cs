using System.Diagnostics;
using TwistedTaleweaver.Core.Logging.Entities;
using Microsoft.Extensions.Logging;

namespace TwistedTaleweaver.Core.Logging;

/// <summary>
/// A DelegatingHandler that logs HTTP requests and responses.
/// It captures request and response details, including headers, body, status codes, and exceptions.
/// </summary>
public class LoggingHandler(ILogger<LoggingHandler> logger) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();

        var logEntry = new HttpLogEntry
        {
            Timestamp = DateTimeOffset.UtcNow,
            Request = await BuildRequestInfoAsync(request, cancellationToken)
        };

        try
        {
            var response = await base.SendAsync(request, cancellationToken);
            stopwatch.Stop();

            logEntry.Response = await BuildResponseInfoAsync(response, cancellationToken);
            logEntry.DurationMs = stopwatch.Elapsed.TotalMilliseconds;

            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            logEntry.Exception = new ExceptionInfo
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                DurationMs = stopwatch.Elapsed.TotalMilliseconds
            };
            throw;
        }
        finally
        {
            if (logEntry.Exception is not null)
            {
                logger.LogError("{@HttpLog}", logEntry);
            }
            else
            {
                logger.LogInformation("{@HttpLog}", logEntry);
            }
        }
    }

    private static async Task<RequestInfo> BuildRequestInfoAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var body = request.Content is not null
            ? await request.Content.ReadAsStringAsync(cancellationToken)
            : null;

        return new RequestInfo
        {
            Method = request.Method.Method,
            Url = request.RequestUri!.ToString(),
            HttpVersion = request.Version.ToString(),
            Headers = request.Headers.ToDictionary(h => h.Key, h => string.Join(", ", h.Value)),
            Body = body,
            ContentLength = body?.Length
        };
    }

    private static async Task<ResponseInfo> BuildResponseInfoAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        return new ResponseInfo
        {
            StatusCode = (int)response.StatusCode,
            ReasonPhrase = response.ReasonPhrase,
            Headers = response.Headers.ToDictionary(h => h.Key, h => string.Join(", ", h.Value)),
            Body = body,
            ContentLength = body.Length
        };

    }
}
