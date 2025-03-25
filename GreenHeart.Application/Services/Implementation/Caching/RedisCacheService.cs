using EasyCaching.Core;
using GreenHeart.Application.Services.Interfaces.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Services.Implementation.Caching
{
    public class RedisCacheService: ICacheService
    {
        private readonly IEasyCachingProvider _provider;

        public RedisCacheService(IEasyCachingProvider provider)
        {
            _provider = provider;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var cacheValue = await _provider.GetAsync<T>(key);
            return cacheValue.HasValue ? cacheValue.Value : default;
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            await _provider.SetAsync(key, value, expiration);
        }

        public async Task RemoveAsync(string key)
        {
            await _provider.RemoveAsync(key);
        }
        public async Task<bool> ExistsAsync(string key)
        {
            return await _provider.ExistsAsync(key);
        }

        public async Task<List<T>> GetListAsync<T>(string key)
        {
            var cacheResult = await _provider.GetAsync<List<T>>(key);
            return cacheResult.HasValue ? cacheResult.Value : new List<T>();
        }

        public async Task SetListAsync<T>(string key, List<T> value, TimeSpan expiration)
        {
            await _provider.SetAsync(key, value, expiration);
        }
    }
}
