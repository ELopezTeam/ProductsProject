using System;
using Microsoft.Extensions.Caching.Memory;
using ProductApi.Domain.Interfaces;

namespace ProductApi.Infrastructure.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public string GetStatusName(int status)
        {
            var cacheKey = "ProductStatus";
            var statusDictionary = _memoryCache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return new Dictionary<int, string>
                {
                    { 0, "Inactive" },
                    { 1, "Active" }
                };
            });

            return statusDictionary.ContainsKey(status) ? statusDictionary[status] : "Unknown";
        }
    }
}