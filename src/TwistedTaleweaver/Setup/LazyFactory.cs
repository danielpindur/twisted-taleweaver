using Microsoft.Extensions.DependencyInjection;

namespace TwistedTaleweaver.Setup;

internal class LazyFactory<T> : Lazy<T> where T : class
{
    public LazyFactory(IServiceProvider provider) : base(provider.GetRequiredService<T>)
    {
    }
}