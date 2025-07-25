using System.Reflection;
using TwistedTaleweaver.Common;
using TwistedTaleweaver.Expeditions.Configurations;
using TwistedTaleweaver.Expeditions.Processors;
using TwistedTaleweaver.Expeditions.Tasks;

namespace TwistedTaleweaver.Setup;

public static class ApplicationBuilderExtensions
{
    public static IServiceCollection AddTwistedTaleweaver(this IHostApplicationBuilder builder)
    {
        var assembly = Assembly.GetCallingAssembly();

        builder.AddConfigurations();
        
        return builder.Services
            .AddFacades(assembly)
            .AddApiClients(assembly)
            .AddSingleton<IDebouncer, Debouncer>()
            .AddTransient<IExpeditionCombatProcessor, ExpeditionCombatProcessor>();
    }
    
    private static IServiceCollection AddFacades(this IServiceCollection services, Assembly assembly)
    {
        var facadeTypes = assembly
            .GetTypes()
            .Where(t =>
                !t.IsAbstract &&
                typeof(IFacade).IsAssignableFrom(t));

        foreach (var type in facadeTypes)
        {
            var @interface = type
                .GetInterfaces()
                .Single(i => i != typeof(IFacade) && typeof(IFacade).IsAssignableFrom(i));
            
            services.AddTransient(@interface, type);
        }

        return services;
    }

    private static IServiceCollection AddApiClients(this IServiceCollection services, Assembly assembly)
    {
        var apiClientTypes = assembly
            .GetTypes()
            .Where(t =>
                !t.IsAbstract &&
                typeof(IApiClient).IsAssignableFrom(t));

        foreach (var type in apiClientTypes)
        {
            var @interface = type
                .GetInterfaces()
                .Single(i => i != typeof(IApiClient) && typeof(IApiClient).IsAssignableFrom(i));
            
            services.AddTransient(@interface, type);
        }

        return services;
    }

    public static IServiceCollection WithHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<ExpeditionBackgroundService>();
        
        return services;
    }

    private static void AddConfigurations(this IHostApplicationBuilder builder)
    {
        builder.Services
            .AddOptions<ExpeditionConfiguration>()
            .Bind(builder.Configuration.GetSection("Expedition"))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}