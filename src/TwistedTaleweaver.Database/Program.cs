using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("Database")
                       ?? throw new InvalidOperationException("Database connection string is required");

builder.Services
    .AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddPostgres()
        .WithGlobalConnectionString(connectionString)
        .ScanIn(typeof(Program).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole());

var host = builder.Build();

using var scope = host.Services.CreateScope();
var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

var command = args.Length > 0 ? args[0].ToLower() : "up";

logger.LogInformation("Running migration command: {Command}", command); 

switch (command)
{
    case "up":
        runner.MigrateUp();
        break;
    case "down":
        if (args.Length > 1 && long.TryParse(args[1], out var version))
            runner.MigrateDown(version);
        else
            runner.MigrateDown(0);
        break;
    default:
        logger.LogInformation("Usage: twistedtaleweaver-database [up|down] [version]");
        return 1;
}

logger.LogInformation("Migration completed successfully");

return 0;