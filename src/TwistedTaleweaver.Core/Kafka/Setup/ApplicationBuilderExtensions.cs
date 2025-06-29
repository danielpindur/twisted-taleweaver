using System.Reflection;
using TwistedTaleweaver.Core.Kafka.Configuration;
using TwistedTaleweaver.Core.Kafka.Consumers;
using TwistedTaleweaver.Core.Kafka.Producers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TwistedTaleweaver.Core.Kafka.Setup;

/// <summary>
/// Extension methods for configuring Kafka messaging in the application builder.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Configures Kafka messaging services in the application builder.
    /// </summary>
    public static IServiceCollection AddKafkaMessaging(this IHostApplicationBuilder builder)
    {
        builder.Services
            .AddOptions<KafkaConfiguration>()
            .Bind(builder.Configuration.GetSection("Kafka"))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        return builder.Services;
    }

    /// <summary>
    /// Adds Kafka producers to the service collection.
    /// </summary>
    public static IServiceCollection WithProducers(this IServiceCollection services)
    {
        return services.AddSingleton<IKafkaEventProducer, KafkaEventProducer>();
    }

    /// <summary>
    /// Adds Kafka consumers to the service collection.
    /// </summary>
    public static IServiceCollection WithConsumers(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        
        return services
            .AddKafkaConsumers(assembly)
            .AddHostedService<KafkaConsumerDispatcher>();
    }
    
    /// <summary>
    /// Registers all Kafka consumers found in the application domain.
    /// </summary>
    private static IServiceCollection AddKafkaConsumers(this IServiceCollection services, Assembly assembly)
    {
        var consumerTypes = assembly
            .GetTypes()
            .Where(t =>
                !t.IsAbstract &&
                typeof(IKafkaEventConsumer).IsAssignableFrom(t));

        foreach (var type in consumerTypes)
        {
            services.AddSingleton(typeof(IKafkaEventConsumer), type);
        }

        return services;
    }
}