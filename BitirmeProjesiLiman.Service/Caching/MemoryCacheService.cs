using Microsoft.Extensions.Caching.Memory;
using System;

namespace BitirmeProjesiLiman.Service.Caching
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T Get<T>(string key)
        {
            _memoryCache.TryGetValue(key, out T value);
            return value;
        }

        public void Set<T>(string key, T value, TimeSpan expirationTime)
        {
            if (value != null)
            {
                _memoryCache.Set(key, value, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expirationTime
                });
            }
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
