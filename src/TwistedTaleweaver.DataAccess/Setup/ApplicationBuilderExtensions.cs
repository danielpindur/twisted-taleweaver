using System.Reflection;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Setup;
using TwistedTaleweaver.DataAccess.Common;

namespace TwistedTaleweaver.DataAccess.Setup;

public static class ApplicationBuilderExtensions
{
    public static IServiceCollection AddDataAccess(this IHostApplicationBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        
        return builder.Services
            .AddSingleton<IDbConnectionFactory, DbConnectionFactory>()
            .AddDatabaseAccessors(assembly);
    }
}