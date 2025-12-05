using Clerk.Net.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TwistedTaleweaver.Integration.Clerk.Users;

namespace TwistedTaleweaver.Integration.Clerk.Setup;

public static class ApplicationBuilderExtensions
{
    public static IServiceCollection AddClerkIntegration(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMemoryCache();
        
        return builder.Services
            .AddIntegrationClient(builder.Configuration)
            .AddClients();
    }

    extension(IServiceCollection services)
    {
        private IServiceCollection AddIntegrationClient(IConfiguration configuration)
        {
            services.AddClerkApiClient(config =>
            {
                config.SecretKey = configuration["Clerk:SecretKey"]!;
            });
            
            return services;
        }
        
        private IServiceCollection AddClients()
        {
            services.AddSingleton<IUsersClient, UsersClient>();

            return services;
        }
    }
}