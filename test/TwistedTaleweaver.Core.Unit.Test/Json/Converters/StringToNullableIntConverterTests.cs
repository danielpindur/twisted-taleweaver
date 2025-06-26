using System.Text.Json;
using TwistedTaleweaver.Core.Json.Converters;
using Shouldly;

namespace TwistedTaleweaver.Core.Unit.Test.Json.Converters;

[TestFixture]
public class StringToNullableIntConverterTests
{
    private readonly StringToNullableIntConverter _converter = new();
    private readonly JsonSerializerOptions _options = new();

    [Test]
    public void Read_WhenNull_ReturnsNull()
    {
        // Arrange
        var json = "null";
        
        // Act
        var result = JsonSerializer.Deserialize<int?>(json, _options);
        
        // Assert
        result.ShouldBeNull();
    }
    
    [TestCase("\"42\"", 42)]
    [TestCase("\"0\"", 0)]
    [TestCase("\"-123\"", -123)]
    public void Read_WhenValidString_ReturnsExpectedValue(string json, int expected)
    {
        // Act
        var result = JsonSerializer.Deserialize<int?>(json, 
            new JsonSerializerOptions { Converters = { _converter } });
        
        // Assert
        result.ShouldBe(expected);
    }
    
    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void Read_WhenEmptyOrWhitespace_ReturnsNull(string? input)
    {
        // Arrange
        var json = input == null ? "null" : $"\"{input}\"";
        
        // Act
        var result = JsonSerializer.Deserialize<int?>(json, 
            new JsonSerializerOptions { Converters = { _converter } });
        
        // Assert
        result.ShouldBeNull();
    }
    
    [TestCase("\"abc\"")]
    [TestCase("\"12.34\"")]
    [TestCase("\"123abc\"")]
    public void Read_WhenInvalidString_ThrowsJsonException(string json)
    {
        // Act
        Action action = () => JsonSerializer.Deserialize<int?>(json, 
            new JsonSerializerOptions { Converters = { _converter } });
        
        // Assert
        Should.Throw<JsonException>(action);
    }
    
    [TestCase("42", 42)]
    [TestCase("0", 0)]
    [TestCase("-123", -123)]
    public void Read_WhenNumber_ReturnsExpectedValue(string json, int expected)
    {
        // Act
        var result = JsonSerializer.Deserialize<int?>(json, 
            new JsonSerializerOptions { Converters = { _converter } });
        
        // Assert
        result.ShouldBe(expected);
    }
    
    [TestCase(42, "\"42\"")]
    [TestCase(0, "\"0\"")]
    [TestCase(-123, "\"-123\"")]
    public void Write_WhenHasValue_WritesStringRepresentation(int? value, string expected)
    {
        // Act
        var result = JsonSerializer.Serialize(value, 
            new JsonSerializerOptions { Converters = { _converter } });
        
        // Assert
        result.ShouldBe(expected);
    }
    
    [Test]
    public void Write_WhenNull_WritesNullValue()
    {
        // Arrange
        int? value = null;
        
        // Act
        var result = JsonSerializer.Serialize(value, 
            new JsonSerializerOptions { Converters = { _converter } });
        
        // Assert
        result.ShouldBe("null");
    }
    
    [Test]
    public void Read_WhenInvalidTokenType_ThrowsJsonException()
    {
        // Arrange
        var json = "true";
        
        // Act
        Action action = () => JsonSerializer.Deserialize<int?>(json, 
            new JsonSerializerOptions { Converters = { _converter } });
        
        // Assert
        var ex = Should.Throw<JsonException>(action);
        ex.Message.ShouldBe("Unexpected token True when parsing int.");
    }
}
