using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TwistedTaleweaver.Core.Commands;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.Core.Queries;

namespace TwistedTaleweaver.Core.Setup;

public static class ApplicationBuilderExtensions
{
    public static IServiceCollection AddCore(this IHostApplicationBuilder builder)
    {
        return builder.Services
            .AddLazyResolution()
            .AddUnitOfWork()
            .AddKafkaProducer();
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
        
        public IServiceCollection AddFluentValidation(Assembly assembly)
        {
            services.Scan(scan => scan
                .FromAssemblies(assembly)
                    .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)), false)
                    .AsImplementedInterfaces(x => x.IsGenericType)
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
            return services;
        }

        private IServiceCollection AddKafkaProducer()
        {
            services.AddSingleton<IKafkaEventProducer, KafkaEventProducer>();
            return services;
        }
    }
}