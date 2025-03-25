using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Services.Interfaces.Caching
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan expiration);
        Task RemoveAsync(string key);
        Task<bool> ExistsAsync(string key);
        Task<List<T>> GetListAsync<T>(string key);
        Task SetListAsync<T>(string key, List<T> value, TimeSpan expiration);
    }
}
