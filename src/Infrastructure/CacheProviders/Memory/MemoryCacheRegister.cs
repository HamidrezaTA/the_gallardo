using Application.Utilities;
using Application.Utilities.CacheProvider;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CacheProviders.Memory;

public static class MemoryCacheRegister
{
    public static void RegisterInMemoryCache(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<ICacheProvider, MemoryCacheProvider>();
    }
}