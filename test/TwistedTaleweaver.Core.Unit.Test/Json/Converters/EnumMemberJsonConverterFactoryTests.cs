using System.Text.Json;
using TwistedTaleweaver.Core.Json.Converters;
using TwistedTaleweaver.Core.Unit.Test.Json.Converters.Entities;
using Shouldly;

namespace TwistedTaleweaver.Core.Unit.Test.Json.Converters;

[TestFixture]
public class EnumMemberJsonConverterFactoryTests
{
    private readonly EnumMemberJsonConverterFactory _factory = new();
    private readonly JsonSerializerOptions _options = new();

    [Test]
    public void CanConvert_WhenTypeIsEnum_ReturnsTrue()
    {
        // Act
        var result = _factory.CanConvert(typeof(TestEnum));

        // Assert
        result.ShouldBeTrue();
    }

    [Test]
    public void CanConvert_WhenTypeIsNullableEnum_ReturnsTrue()
    {
        // Act
        var result = _factory.CanConvert(typeof(TestEnum?));

        // Assert
        result.ShouldBeTrue();
    }

    [Test]
    public void CanConvert_WhenTypeIsNotEnum_ReturnsFalse()
    {
        // Act
        var result = _factory.CanConvert(typeof(string));

        // Assert
        result.ShouldBeFalse();
    }

    [Test]
    public void CreateConverter_WhenTypeIsEnum_CreatesNonNullableConverter()
    {
        // Act
        var converter = _factory.CreateConverter(typeof(TestEnum), _options);

        // Assert
        converter.ShouldNotBeNull();
        converter.ShouldBeOfType<EnumMemberJsonConverter<TestEnum>>();
    }

    [Test]
    public void CreateConverter_WhenTypeIsNullableEnum_CreatesNullableConverter()
    {
        // Act
        var converter = _factory.CreateConverter(typeof(TestEnum?), _options);

        // Assert
        converter.ShouldNotBeNull();
        converter.ShouldBeOfType<NullableEnumMemberJsonConverter<TestEnum>>();
    }

    [Test]
    public void NonNullableConverter_Read_WhenNull_ThrowsJsonException()
    {
        // Arrange
        var options = new JsonSerializerOptions { Converters = { _factory } };
        var json = "null";

        // Act
        Action action = () => JsonSerializer.Deserialize<TestEnum>(json, options);

        // Assert
        var ex = Should.Throw<JsonException>(action);
        ex.Message.ShouldBe("The JSON value could not be converted to non-nullable enum.");
    }

    [Test]
    public void NonNullableConverter_Read_WhenInvalidValue_ThrowsJsonException()
    {
        // Arrange
        var options = new JsonSerializerOptions { Converters = { _factory } };
        var json = "\"invalid-value\"";

        // Act
        Action action = () => JsonSerializer.Deserialize<TestEnum>(json, options);

        // Assert
        var ex = Should.Throw<JsonException>(action);
        ex.Message.ShouldBe("Unknown value 'invalid-value' for enum 'TestEnum'");
    }

    [TestCase("\"first\"", TestEnum.First)]
    [TestCase("\"second-value\"", TestEnum.Second)]
    public void NonNullableConverter_Read_WhenValidEnumMemberValue_ReturnsExpectedEnum(string json, TestEnum expected)
    {
        // Arrange
        var options = new JsonSerializerOptions { Converters = { _factory } };

        // Act
        var result = JsonSerializer.Deserialize<TestEnum>(json, options);

        // Assert
        result.ShouldBe(expected);
    }

    [Test]
    public void NonNullableConverter_Read_WhenEnumNameWithoutAttribute_UsesEnumName()
    {
        // Arrange
        var options = new JsonSerializerOptions { Converters = { _factory } };
        var json = "\"Third\"";

        // Act
        var result = JsonSerializer.Deserialize<TestEnum>(json, options);

        // Assert
        result.ShouldBe(TestEnum.Third);
    }

    [Test]
    public void NullableConverter_Read_WhenNull_ReturnsNull()
    {
        // Arrange
        var options = new JsonSerializerOptions { Converters = { _factory } };
        var json = "null";

        // Act
        var result = JsonSerializer.Deserialize<TestEnum?>(json, options);

        // Assert
        result.ShouldBeNull();
    }

    [Test]
    public void NullableConverter_Read_WhenInvalidValue_ReturnsNull()
    {
        // Arrange
        var options = new JsonSerializerOptions { Converters = { _factory } };
        var json = "\"invalid-value\"";

        // Act
        var result = JsonSerializer.Deserialize<TestEnum?>(json, options);

        // Assert
        result.ShouldBeNull();
    }

    [TestCase("\"first\"", TestEnum.First)]
    [TestCase("\"second-value\"", TestEnum.Second)]
    public void NullableConverter_Read_WhenValidEnumMemberValue_ReturnsExpectedEnum(string json, TestEnum? expected)
    {
        // Arrange
        var options = new JsonSerializerOptions { Converters = { _factory } };

        // Act
        var result = JsonSerializer.Deserialize<TestEnum?>(json, options);

        // Assert
        result.ShouldBe(expected);
    }

    [TestCase(TestEnum.First, "\"first\"")]
    [TestCase(TestEnum.Second, "\"second-value\"")]
    [TestCase(TestEnum.Third, "\"Third\"")]
    public void NonNullableConverter_Write_WritesExpectedValue(TestEnum value, string expected)
    {
        // Arrange
        var options = new JsonSerializerOptions { Converters = { _factory } };

        // Act
        var json = JsonSerializer.Serialize(value, options);

        // Assert
        json.ShouldBe(expected);
    }

    [TestCase(TestEnum.First, "\"first\"")]
    [TestCase(TestEnum.Second, "\"second-value\"")]
    [TestCase(TestEnum.Third, "\"Third\"")]
    public void NullableConverter_Write_WhenHasValue_WritesExpectedValue(TestEnum value, string expected)
    {
        // Arrange
        var options = new JsonSerializerOptions { Converters = { _factory } };
        TestEnum? nullableValue = value;

        // Act
        var json = JsonSerializer.Serialize(nullableValue, options);

        // Assert
        json.ShouldBe(expected);
    }

    [Test]
    public void NullableConverter_Write_WhenNull_WritesNullValue()
    {
        // Arrange
        var options = new JsonSerializerOptions { Converters = { _factory } };
        TestEnum? value = null;

        // Act
        var json = JsonSerializer.Serialize(value, options);

        // Assert
        json.ShouldBe("null");
    }
}
