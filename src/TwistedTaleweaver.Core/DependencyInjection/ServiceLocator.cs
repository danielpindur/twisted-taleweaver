using Microsoft.Extensions.DependencyInjection;

namespace TwistedTaleweaver.Core.DependencyInjection;

internal static class ServiceLocator
{
    private static IServiceProvider? _serviceProvider;
    
    public static void Initialize(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }
    
    public static T GetService<T>() where T : notnull
    {
        if (_serviceProvider == null)
        {
            throw new InvalidOperationException($"{nameof(IServiceProvider)} is not initialized.");
        }

        return _serviceProvider.GetRequiredService<T>();
    }
}