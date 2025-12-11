using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TwistedTaleweaver.Core.Commands;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Database.UnitOfWork;
using TwistedTaleweaver.Core.Queries;

namespace TwistedTaleweaver.Core.Setup;

public static class ApplicationBuilderExtensions
{
    public static IServiceCollection AddCore(this IHostApplicationBuilder builder)
    {
        return builder.Services
            .AddLazyResolution()
            .AddUnitOfWork();
    }

    extension(IServiceCollection services)
    {
        public IServiceCollection AddCommandHandlers(Assembly assembly)
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

        public IServiceCollection AddQueryHandlers(Assembly assembly)
        {
            services.Scan(scan => scan
                .FromAssemblies(assembly)
                    .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), false)
                        .AsImplementedInterfaces(x => !x.IsGenericType)
                        .WithTransientLifetime());

            return services;
        }

        public IServiceCollection AddDatabaseAccessors(Assembly assembly)
        {
            services.Scan(scan => scan
                .FromAssemblies(assembly)
                    .AddClasses(classes => classes.AssignableTo(typeof(IDatabaseAccessor)), false)
                    .AsImplementedInterfaces(x => x != typeof(IDatabaseAccessor))
                    .WithTransientLifetime());
            
            return services;
        }
        
        private IServiceCollection AddLazyResolution()
        {
            services.AddTransient(typeof(Lazy<>), typeof(LazyFactory<>));
            return services;
        }

        private IServiceCollection AddUnitOfWork()
        {
            services.AddScoped<IUnitOfWorkProvider, UnitOfWorkProvider>();
            services.AddTransient<IUnitOfWork>(serviceProvider =>
            {
                var unitOfWorkProvider = serviceProvider.GetRequiredService<IUnitOfWorkProvider>();
                return unitOfWorkProvider.GetActiveUnitOfWork();
            });
            
            return services;
        }
    }
}