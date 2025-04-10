using GreenHeart.Application.Services.Interfaces.Caching;
using GreenHeart.Application.Statics.Caches_Constatnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Extensions
{
    public static class CacheExtensions
    {
        public static async Task InvalidateCacheKey(this ICacheService cacheService, string cacheKey)
        {
            if (cacheKey.Contains("*"))
                await cacheService.RemovePatternAsync(cacheKey);
            else
                await cacheService.RemoveAsync(cacheKey);
        }
        public static async Task InvalidateCacheKey(this ICacheService cacheService, string[] cacheKeys)
        {
            foreach (var cacheKey in cacheKeys)
            {
                if(cacheKey.Contains("*"))
                    await cacheService.RemovePatternAsync(cacheKey);
                else
                    await cacheService.RemoveAsync(cacheKey);
            }            
        }
    }
}
