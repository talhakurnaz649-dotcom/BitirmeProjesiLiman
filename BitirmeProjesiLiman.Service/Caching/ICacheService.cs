using System;

namespace BitirmeProjesiLiman.Service.Caching
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan expirationTime);
        void Remove(string key);
    }
}
