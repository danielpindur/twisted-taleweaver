using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.Core.Kafka.Extensions;
using Shouldly;

namespace TwistedTaleweaver.Core.Unit.Test.Kafka.Extensions;

[TestFixture]
public class KafkaEventExtensionsTests
{
    private class TestKafkaEvent : KafkaEvent
    {
        public TestKafkaEvent(string eventType, string topic, Type payloadType) : base(eventType, topic, payloadType)
        {
        }
    }

    private class TestPayload : EventPayload
    {
        public string Data { get; set; } = string.Empty;
    }

    private class DifferentTestPayload : EventPayload
    {
        public int Value { get; set; }
    }

    [Test]
    public void WithPayload_WhenPayloadTypeMatches_ReturnsNewInstanceWithPayload()
    {
        // Arrange
        var eventType = "test-event";
        var topic = "test-topic";
        var payload = new TestPayload { Data = "test data", NotificationMessageId = Guid.NewGuid().ToString() };
        var kafkaEvent = new TestKafkaEvent(eventType, topic, typeof(TestPayload));

        // Act
        var result = kafkaEvent.WithPayload(payload);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<TestKafkaEvent>();
        result.ShouldNotBeSameAs(kafkaEvent); // Should be a new instance
        result.EventType.ShouldBe(eventType);
        result.Topic.ShouldBe(topic);
        result.PayloadType.ShouldBe(typeof(TestPayload));
    }

    [Test]
    public void WithPayload_WhenPayloadTypeMismatch_ThrowsArgumentException()
    {
        // Arrange
        var eventType = "test-event";
        var topic = "test-topic";
        var payload = new DifferentTestPayload { Value = 42, NotificationMessageId = Guid.NewGuid().ToString() };
        var kafkaEvent = new TestKafkaEvent(eventType, topic, typeof(TestPayload));

        // Act & Assert
        var ex = Should.Throw<ArgumentException>(() => kafkaEvent.WithPayload(payload));
        ex.Message.ShouldBe($"Payload type mismatch. Expected {typeof(TestPayload)}, got {typeof(DifferentTestPayload)}");
    }
}
