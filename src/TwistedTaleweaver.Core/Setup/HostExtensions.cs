using Microsoft.Extensions.Hosting;
using TwistedTaleweaver.Core.DependencyInjection;

namespace TwistedTaleweaver.Core.Setup;

public static class HostExtensions
{
    public static void ConfigureCore(this IHost builder)
    {
        builder.InitialiseServiceLocator();
    }
    
    extension(IHost builder)
    {
        private IHost InitialiseServiceLocator()
        {
            ServiceLocator.Initialize(builder.Services);
        
            return builder;
        }
    }
}