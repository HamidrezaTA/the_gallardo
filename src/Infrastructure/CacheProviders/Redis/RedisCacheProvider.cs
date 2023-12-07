using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Utilities.CacheProvider;
using Application.Utilities.Logger;
using StackExchange.Redis;

namespace Infrastructure.CacheProviders.Redis;


public class RedisCacheProvider : ICacheProvider
{
    private readonly IDatabase _database;
    private readonly IApplicationLogger<RedisCacheProvider> _logger;
    public RedisCacheProvider(string connectionString, IApplicationLogger<RedisCacheProvider> logger)
    {
        var connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
        _database = connectionMultiplexer.GetDatabase();
        _logger = logger;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var serializedData = await _database.StringGetAsync(key);

        if (!serializedData.IsNull)
            return default;

        using (var stream = new MemoryStream(serializedData))
        {
            return await JsonSerializer.DeserializeAsync<T>(stream);
        }
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
    {
        var serializedData = JsonSerializer.Serialize(value);
        await _database.StringSetAsync(key, serializedData, expiration);
    }

    public async Task<bool> RemoveAsync(string key)
    {
        return await _database.KeyDeleteAsync(key);
    }

    public async Task<bool> ExistsAsync(string key)
    {
        return await _database.KeyExistsAsync(key);
    }

    public async Task<T?> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> getDataFunc, TimeSpan cacheDuration)
    {
        try
        {
            var keyExist = await ExistsAsync(cacheKey);
            if (keyExist)
            {
                _logger.LogDebug("Value for {@CacheKey} was fetched from Cache", cacheKey);
                return await GetAsync<T>(cacheKey);
            }

            var cachedData = await getDataFunc();
            if (cachedData != null)
            {
                await SetAsync(cacheKey, cachedData, cacheDuration);
                _logger.LogDebug("Value for {@CacheKey} was set in Cache", cacheKey);
            }

            return cachedData;
        }
        catch (RedisException)
        {
            return await getDataFunc();
        }
    }
}