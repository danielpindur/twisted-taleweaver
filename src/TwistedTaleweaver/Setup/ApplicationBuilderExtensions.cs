using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TwistedTaleweaver.Common.CommandHandlers;
using TwistedTaleweaver.Common.DataAccess;
using TwistedTaleweaver.Common.QueryHandlers;

namespace TwistedTaleweaver.Setup;

public static class ApplicationBuilderExtensions
{
    public static IServiceCollection AddTwistedTaleweaver(this IHostApplicationBuilder builder)
    {
        var assembly = Assembly.GetCallingAssembly();

        return builder.Services
            .AddDataAccess()
            .AddCommandHandlers(assembly)
            .AddQueryHandlers(assembly);
    }

    extension(IServiceCollection services)
    {
        private IServiceCollection AddCommandHandlers(Assembly assembly)
        {
            var commandHandlerTypes = assembly
                .GetTypes()
                .Where(t =>
                    !t.IsAbstract &&
                    (typeof(ICommandHandler<>).IsAssignableFrom(t) || typeof(ICommandHandler<,>).IsAssignableFrom(t)));

            foreach (var type in commandHandlerTypes)
            {
                var @interface = type
                    .GetInterfaces()
                    .Single(i =>
                        (i != typeof(ICommandHandler<>) && typeof(ICommandHandler<>).IsAssignableFrom(i)) ||
                        (i != typeof(ICommandHandler<,>) && typeof(ICommandHandler<>).IsAssignableFrom(i)));
            
                services.AddTransient(@interface, type);
            }

            return services;
        }

        private IServiceCollection AddQueryHandlers(Assembly assembly)
        {
            var queryHandlerTypes = assembly
                .GetTypes()
                .Where(t =>
                    !t.IsAbstract &&
                    typeof(IQueryHandler<,>).IsAssignableFrom(t));

            foreach (var type in queryHandlerTypes)
            {
                var @interface = type
                    .GetInterfaces()
                    .Single(i => i != typeof(IQueryHandler<,>) && typeof(IQueryHandler<,>).IsAssignableFrom(i));
            
                services.AddTransient(@interface, type);
            }

            return services;
        }

        private IServiceCollection AddDataAccess()
        {
            services
                .AddSingleton<IDbConnectionFactory, DbConnectionFactory>()
                .AddTransient<Func<IUnitOfWork>>(provider =>
                    () => new UnitOfWork(provider.GetRequiredService<IDbConnectionFactory>()));

            return services;
        }
    }
}