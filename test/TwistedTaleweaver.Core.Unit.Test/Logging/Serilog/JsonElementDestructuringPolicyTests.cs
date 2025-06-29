using System.Text.Json;
using TwistedTaleweaver.Core.Logging.Serilog;
using Moq;
using Serilog.Events;
using Serilog.Core;
using Shouldly;

namespace TwistedTaleweaver.Core.Unit.Test.Logging.Serilog;

[TestFixture]
public class JsonElementDestructuringPolicyTests
{
    private readonly JsonElementDestructuringPolicy _policy = new();
    private readonly ILogEventPropertyValueFactory _factory = new Mock<ILogEventPropertyValueFactory>().Object;

    [Test]
    public void TryDestructure_WhenValueIsNull_ReturnsFalse()
    {
        // Act
        var result = _policy.TryDestructure(null, _factory, out var propertyValue);

        // Assert
        result.ShouldBeFalse();
        propertyValue.ShouldBeNull();
    }

    [Test]
    public void TryDestructure_WhenValueIsNotJsonElement_ReturnsFalse()
    {
        // Arrange
        var nonJsonElement = "not a json element";

        // Act
        var result = _policy.TryDestructure(nonJsonElement, _factory, out var propertyValue);

        // Assert
        result.ShouldBeFalse();
        propertyValue.ShouldBeNull();
    }

    [Test]
    public void TryDestructure_WhenValueIsJsonElement_ReturnsTrue()
    {
        // Arrange
        var jsonString = "{\"name\":\"test\"}";
        using var jsonDocument = JsonDocument.Parse(jsonString);
        var jsonElement = jsonDocument.RootElement;

        // Act
        var result = _policy.TryDestructure(jsonElement, _factory, out var propertyValue);

        // Assert
        result.ShouldBeTrue();
        propertyValue.ShouldNotBeNull();
    }

    [Test]
    public void TryDestructure_WhenValueIsJsonElement_ReturnsCorrectStringRepresentation()
    {
        // Arrange
        var jsonString = "{\"name\":\"test\",\"value\":123}";
        using var jsonDocument = JsonDocument.Parse(jsonString);
        var jsonElement = jsonDocument.RootElement;

        // Act
        _policy.TryDestructure(jsonElement, _factory, out var propertyValue);

        // Assert
        propertyValue.ShouldBeOfType<ScalarValue>();
        ((ScalarValue)propertyValue).Value.ShouldBe(jsonString);
    }
}
