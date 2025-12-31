using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            .AddKafkaProducer()
            .AddKafkaEventDispatcher();
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

        private IServiceCollection AddKafkaEventDispatcher()
        {
            services.AddSingleton<IKafkaEventDispatcher, KafkaEventDispatcher>();
            return services;
        }

        public IServiceCollection AddKafkaEventConsumers(Assembly assembly)
        {
            services.Scan(scan => scan
                .FromAssemblies(assembly)
                    .AddClasses(classes => classes.AssignableTo(typeof(IKafkaEventConsumer<>)), false)
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
            
            return services;
        }

        public IServiceCollection SubscribeToEvent<T>() where T : class
        {
            services.AddHostedService<KafkaSubscriber<T>>(provider =>
            {
                var dispatcher = provider.GetRequiredService<IKafkaEventDispatcher>();
                var logger = provider.GetRequiredService<ILogger<KafkaSubscriber<T>>>();
                var configuration = provider.GetRequiredService<IConfiguration>();
                
                return new KafkaSubscriber<T>(dispatcher, logger, configuration);
            });

            return services;
        }
    }
}