using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace BistroFiftyTwo.Server.Services
{
    public class CacheService : ICacheService
    {
        public CacheService(IDistributedCache distributedCache)
        {
            DistributedCache = distributedCache;
        }

        protected IDistributedCache DistributedCache { get; set; }

        public async Task SetAsync<T>(string key, T entity, double durationMs) where T : class
        {
            var expiry = TimeSpan.FromMilliseconds(durationMs);

            var strRep = JsonConvert.SerializeObject(entity);
            var cacheBytes = Encoding.UTF8.GetBytes(strRep);
            await DistributedCache.SetAsync(key, cacheBytes,
                new DistributedCacheEntryOptions {AbsoluteExpirationRelativeToNow = expiry});
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var data = await DistributedCache.GetAsync(key);

            if (data == null) return default(T);

            var strRep = Encoding.UTF8.GetString(data);

            return JsonConvert.DeserializeObject<T>(strRep);
        }
    }
}