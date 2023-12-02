using Application.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Caching.Memory;

public static class MemoryCacheRegistration
{
    public static void AddInMemoryCache(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<ICacheProvider, MemoryCacheProvider>();
    }
}