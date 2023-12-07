using Application.Utilities.CacheProvider;
using Application.Utilities.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CacheProviders.Redis;

public static class RedisRegister
{
    public static void RegisterRedis(this IServiceCollection services, string redisConnectionString)
    {
        services.AddSingleton<ICacheProvider>(sp => new RedisCacheProvider(redisConnectionString, sp.CreateScope().ServiceProvider
                                                                                                    .GetRequiredService<IApplicationLogger<RedisCacheProvider>>()));
    }
}