using System.Net;
using TwistedTaleweaver.Core.Logging;
using TwistedTaleweaver.Core.Logging.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace TwistedTaleweaver.Core.Unit.Test.Logging;

public class LoggingHandlerTests
{
    private Mock<ILogger<LoggingHandler>> _loggerMock;
    private LoggingHandler _handler;
    private TestMessageHandler _innerHandler;

    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<LoggingHandler>>();
        _innerHandler = new TestMessageHandler();
        _handler = new LoggingHandler(_loggerMock.Object)
        {
            InnerHandler = _innerHandler
        };
    }

    [TearDown]
    public void TearDown()
    {
        _handler.Dispose();
        _innerHandler.Dispose();
    }

    [Test]
    public async Task SendAsync_SuccessfulRequest_LogsRequestAndResponse()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "https://api.example.com/test");
        var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("Success response")
        };
        
        _innerHandler.Response = expectedResponse;

        // Act
        var invoker = new HttpMessageInvoker(_handler);
        var response = await invoker.SendAsync(request, CancellationToken.None);

        // Assert
        response.ShouldNotBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => 
                    IsValidHttpLogEntry(v, request, response)),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Test]
    public async Task SendAsync_WhenExceptionOccurs_LogsException()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "https://api.example.com/test");
        var expectedException = new HttpRequestException("Test exception");
        _innerHandler.ExceptionToThrow = expectedException;

        // Act & Assert
        var invoker = new HttpMessageInvoker(_handler);
        await Should.ThrowAsync<HttpRequestException>(() => invoker.SendAsync(request, CancellationToken.None));

        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => 
                    IsValidHttpLogEntryWithException(v, request, expectedException)),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Test]
    public async Task SendAsync_CapturesRequestHeaders()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "https://api.example.com/test");
        request.Headers.Add("X-Test-Header", "test-value");
        var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);
        _innerHandler.Response = expectedResponse;

        // Act
        var invoker = new HttpMessageInvoker(_handler);
        await invoker.SendAsync(request, CancellationToken.None);

        // Assert
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => 
                    HasRequestHeader(v, "X-Test-Header", "test-value")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Test]
    public async Task SendAsync_CapturesResponseHeaders()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "https://api.example.com/test");
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        response.Headers.Add("X-Response-Header", "response-value");
        _innerHandler.Response = response;

        // Act
        var invoker = new HttpMessageInvoker(_handler);
        await invoker.SendAsync(request, CancellationToken.None);

        // Assert
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => 
                    HasResponseHeader(v, "X-Response-Header", "response-value")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Test]
    public async Task SendAsync_CaptureDuration()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "https://api.example.com/test");
        _innerHandler.DelayMilliseconds = 100;
        _innerHandler.Response = new HttpResponseMessage(HttpStatusCode.OK);

        // Act
        var invoker = new HttpMessageInvoker(_handler);
        await invoker.SendAsync(request, CancellationToken.None);

        // Assert
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => 
                    HasMinimumDuration(v, 100)),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    private static bool IsValidHttpLogEntry(object value, HttpRequestMessage request, HttpResponseMessage response)
    {
        var formattedLogValues = value as IEnumerable<KeyValuePair<string, object>>;
        var logEntry = formattedLogValues!.First().Value as HttpLogEntry;
        
        if (logEntry == null)
        {
            return false;
        }

        return logEntry.Request.Method == request.Method.ToString()
               && logEntry.Request.Url == request.RequestUri?.ToString()
               && logEntry.Response != null
               && logEntry.Response.StatusCode == (int)response.StatusCode
               && logEntry.DurationMs.HasValue;
    }
    
    private static bool IsValidHttpLogEntryWithException(object value, HttpRequestMessage request, Exception exception)
    {
        var formattedLogValues = value as IEnumerable<KeyValuePair<string, object>>;
        var logEntry = formattedLogValues!.First().Value as HttpLogEntry;

        return logEntry != null
               && logEntry.Request.Method == request.Method.ToString()
               && logEntry.Request.Url == request.RequestUri?.ToString()
               && logEntry.Exception is not null
               && logEntry.Exception.Message == exception.Message;
    }

    private static bool HasRequestHeader(object value, string headerName, string headerValue)
    {
        var formattedLogValues = value as IEnumerable<KeyValuePair<string, object>>;
        var logEntry = formattedLogValues!.First().Value as HttpLogEntry;
        
        return logEntry?.Request.Headers.ContainsKey(headerName) == true 
            && logEntry.Request.Headers[headerName].Contains(headerValue);
    }

    private static bool HasResponseHeader(object value, string headerName, string headerValue)
    {
        var formattedLogValues = value as IEnumerable<KeyValuePair<string, object>>;
        var logEntry = formattedLogValues!.First().Value as HttpLogEntry;
        
        return logEntry?.Response?.Headers.ContainsKey(headerName) == true 
            && logEntry.Response.Headers[headerName].Contains(headerValue);
    }

    private static bool HasMinimumDuration(object value, int minimumDuration)
    {
        var formattedLogValues = value as IEnumerable<KeyValuePair<string, object>>;
        var logEntry = formattedLogValues!.First().Value as HttpLogEntry;
        
        return logEntry?.DurationMs >= minimumDuration;
    }
    
    private class TestMessageHandler : HttpMessageHandler
    {
        public HttpResponseMessage? Response { get; set; }
        public Exception? ExceptionToThrow { get; set; }
        public int DelayMilliseconds { get; set; }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (DelayMilliseconds > 0)
            {
                await Task.Delay(DelayMilliseconds, cancellationToken);
            }

            if (ExceptionToThrow != null)
            {
                throw ExceptionToThrow;
            }

            return Response ?? new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}