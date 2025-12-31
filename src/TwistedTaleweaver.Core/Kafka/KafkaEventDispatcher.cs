using Microsoft.Extensions.DependencyInjection;

namespace TwistedTaleweaver.Core.Kafka;

public interface IKafkaEventDispatcher
{
    Task DispatchAsync(object @event, CancellationToken cancellationToken = default);
}

public class KafkaEventDispatcher(IServiceProvider serviceProvider) : IKafkaEventDispatcher
{
    public async Task DispatchAsync(object @event, CancellationToken cancellationToken = default)
    {
        using var scope = serviceProvider.CreateScope();

        var consumers = scope.ServiceProvider.GetServices<IKafkaEventConsumer>();

        var eventType = @event.GetType();
        var consumerInterfaceType = typeof(IKafkaEventConsumer<>).MakeGenericType(eventType);

        foreach (var consumer in consumers)
        {
            if (consumerInterfaceType.IsInstanceOfType(consumer))
            {
                var method = consumerInterfaceType.GetMethod("ConsumeAsync")!;
                await (Task)method.Invoke(consumer, [@event, cancellationToken])!;
            }
        }
    }
}
