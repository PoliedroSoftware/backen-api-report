using Microsoft.Extensions.Options;
using Poliedro.Report.Application.Ports.Redis;
using StackExchange.Redis;
using System.Text.Json;

namespace Poliedro.Report.Infraestructure.Persistence.Mysql.Redis;

public class RedisCacheService : IRedisService
{
    private readonly ConnectionMultiplexer _redis;
    private readonly StackExchange.Redis.IDatabase _db;

    public RedisCacheService(IOptions<RedisConfig> config)
    {
        _redis = ConnectionMultiplexer.Connect(config.Value.ConnectionString);
        _db = _redis.GetDatabase();
    }

    public async Task SetCacheAsync<T>(string key, T value, TimeSpan expiration)
    {
        try
        {
            var json = JsonSerializer.Serialize(value);
            await _db.StringSetAsync(key, json, expiration);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[General error] getting cache: {ex.Message}");
        }
       
    }

    public async Task<T?> GetCacheAsync<T>(string key)
    {
        try
        {
            var json = await _db.StringGetAsync(key);
            return json.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[General error] getting cache: {ex.Message}");
            return default;
        }
       
    }

    public async Task<bool> RemoveCacheAsync(string key)
    {
        return await _db.KeyDeleteAsync(key);
    }

    public async Task RemoveByPrefixAsync(string prefix)
    {
        var server = _redis.GetServer(_redis.GetEndPoints()[0]);
        var keysToDelete = new List<RedisKey>();

        await foreach (var key in server.KeysAsync(pattern: $"{prefix}*"))
        {
            keysToDelete.Add(key);
        }

        if (keysToDelete.Count > 0)
        {
            await _db.KeyDeleteAsync(keysToDelete.ToArray());
        }
    }
}
