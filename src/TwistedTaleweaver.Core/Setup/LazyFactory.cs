using Microsoft.Extensions.DependencyInjection;

namespace TwistedTaleweaver.Core.Setup;

internal class LazyFactory<T> : Lazy<T> where T : class
{
    public LazyFactory(IServiceProvider provider) : base(provider.GetRequiredService<T>)
    {
    }
}