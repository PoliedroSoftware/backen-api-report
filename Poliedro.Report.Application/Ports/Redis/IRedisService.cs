namespace Poliedro.Report.Application.Ports.Redis;

public interface IRedisService
{
    Task SetCacheAsync<T>(string key, T value, TimeSpan expiration);
    Task<T?> GetCacheAsync<T>(string key);
    Task<bool> RemoveCacheAsync(string key);
    Task RemoveByPrefixAsync(string prefix);
}
