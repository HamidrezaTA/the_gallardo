using System;
using System.Threading.Tasks;

namespace Application.Utilities;

public interface ICacheProvider
{
    Task<T> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> getDataFunc, TimeSpan cacheDuration);
    Task<T> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan expiration);
    Task<bool> RemoveAsync(string key);
    Task<bool> ExistsAsync(string key);
}