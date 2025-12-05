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
        var assembly = Assembly.GetExecutingAssembly();

        return builder.Services
            .AddDataAccess()
            .AddCommandHandlers(assembly)
            .AddQueryHandlers(assembly);
    }

    extension(IServiceCollection services)
    {
        private IServiceCollection AddCommandHandlers(Assembly assembly)
        {
            services.Scan(scan => scan
                .FromAssemblies(assembly)
                    .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), false)
                        .AsImplementedInterfaces(x => !x.IsGenericType)
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), false)
                        .AsImplementedInterfaces(x => !x.IsGenericType)
                        .WithTransientLifetime());

            return services;
        }

        private IServiceCollection AddQueryHandlers(Assembly assembly)
        {
            services.Scan(scan => scan
                .FromAssemblies(assembly)
                    .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), false)
                        .AsImplementedInterfaces(x => !x.IsGenericType)
                        .WithTransientLifetime());

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