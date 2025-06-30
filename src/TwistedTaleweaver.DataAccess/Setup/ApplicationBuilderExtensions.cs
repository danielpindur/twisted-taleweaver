using System.Reflection;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Common.TypeHandlers;
using TwistedTaleweaver.DataAccess.Configuration;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TwistedTaleweaver.DataAccess.Setup;

public static class ApplicationBuilderExtensions
{
    public static IServiceCollection AddDataAccess(this IHostApplicationBuilder builder)
    {
        SqlMapper.AddTypeHandler(new JsonDocumentTypeHandler());
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        var assembly = Assembly.GetExecutingAssembly();
        
        builder.Services
            .AddOptions<DataAccessConfiguration>()
            .Bind(builder.Configuration.GetSection("DataAccess"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        builder.Services
            .AddRepositories(assembly)
            .AddSingleton<IDbConnectionFactory, DbConnectionFactory>()
            .AddTransient<Func<IUnitOfWork>>(provider =>
                () => new UnitOfWork(provider.GetRequiredService<IDbConnectionFactory>()));
        
        return builder.Services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services, Assembly assembly)
    {
        var repositoryTypes = assembly
            .GetTypes()
            .Where(t =>
                !t.IsAbstract &&
                typeof(IRepository).IsAssignableFrom(t));

        foreach (var type in repositoryTypes)
        {
            var @interface = type
                .GetInterfaces()
                .Single(i => i != typeof(IRepository) && typeof(IRepository).IsAssignableFrom(i));
            
            services.AddTransient(@interface, type);
        }

        return services;
    }
}