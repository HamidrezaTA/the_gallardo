using System;
using System.Threading.Tasks;
using Application.Utilities.CacheProvider;
using Microsoft.Extensions.Caching.Memory;


namespace Infrastructure.CacheProviders.Memory;

public class MemoryCacheProvider : ICacheProvider
{
    private readonly IMemoryCache _database;

    public MemoryCacheProvider(IMemoryCache database)
    {
        _database = database;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = _database.Get<T>(key);
        return value;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
    {
        _database.Set(key, value, expiration);
    }
    public Task<bool> RemoveAsync(string key)
    {
        _database.Remove(key);
        return Task.FromResult(true);
    }
    public async Task<bool> ExistsAsync(string key)
    {
        var result = await GetAsync<object>(key);
        if (result == null)
        {
            return await Task.FromResult(false);
        }

        return await Task.FromResult(true);
    }

    public async Task<T?> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> getDataFunc, TimeSpan cacheDuration)
    {
        try
        {
            var keyExist = await ExistsAsync(cacheKey);
            if (keyExist)
            {
                return await GetAsync<T>(cacheKey);
            }

            var cachedData = await getDataFunc();
            if (cachedData != null)
            {
                await SetAsync(cacheKey, cachedData, cacheDuration);
            }

            return cachedData;
        }
        catch (Exception)
        {
            return await getDataFunc();
        }
    }
}