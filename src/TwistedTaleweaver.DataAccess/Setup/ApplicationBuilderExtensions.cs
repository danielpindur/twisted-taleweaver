using System.Reflection;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Setup;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Common.TypeHandlers;

namespace TwistedTaleweaver.DataAccess.Setup;

public static class ApplicationBuilderExtensions
{
    public static IServiceCollection AddDataAccess(this IHostApplicationBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly();

        DefaultTypeMap.MatchNamesWithUnderscores = true;
        AddTypeHandlers();

        return builder.Services
            .AddSingleton<IDbConnectionFactory, DbConnectionFactory>()
            .AddDatabaseAccessors(assembly);
    }

    private static void AddTypeHandlers()
    {
        SqlMapper.AddTypeHandler(new JsonTypeHandler<List<CreatureTurn>>());
    }
}