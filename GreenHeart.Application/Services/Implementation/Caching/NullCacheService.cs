using GreenHeart.Application.Services.Interfaces.Caching;
using GreenHeart.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Services.Implementation.Caching
{
    public class NullCacheService : ICacheService
    {
        private readonly bool _usingCache;

        public NullCacheService(IConfiguration configuration)
        {
            _usingCache = configuration.GetValue<bool>("Statics:UseCaching");
        }

        public Task<bool> ExistsAsync(string key)
        {
            // Since caching is disabled, keys never "exist" in cache
            return Task.FromResult(false);
        }

        public Task<T> GetAsync<T>(string key)
        {
            // Return default value (e.g., null for reference types, 0 for int, etc.)
            return Task.FromResult(default(T));
        }

        public Task<List<T>> GetListAsync<T>(string key)
        {
            // Return an empty list instead of null (better for callers)
            return Task.FromResult(new List<T>());
        }

        public Task RemoveAsync(string key)
        {
            // Do nothing (cache is disabled)
            return Task.CompletedTask;
        }

        public Task RemovePatternAsync(string patternKey)
        {
            // Do nothing (cache is disabled)
            return Task.CompletedTask;
        }

        public Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            // Do nothing (cache is disabled)
            return Task.CompletedTask;
        }

        public Task SetListAsync<T>(string key, List<T> value, TimeSpan expiration)
        {
            // Do nothing (cache is disabled)
            return Task.CompletedTask;
        }
    }
}
