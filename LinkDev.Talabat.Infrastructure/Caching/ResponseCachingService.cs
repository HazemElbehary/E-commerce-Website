using LinkDev.Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure.Caching;
using StackExchange.Redis;
using System.Text.Json;

namespace LinkDev.Talabat.Infrastructure.Caching
{
    internal class ResponseCachingService(IConnectionMultiplexer connectionMultiplexer) : IResponseCachingService
    {
        IDatabase database = connectionMultiplexer.GetDatabase();
        public async Task CachResponseAsync(string key, object response, TimeSpan timeToLive)
        {
            if (response is null) return;

            var serlizationOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var value = JsonSerializer.Serialize(response, serlizationOptions);
            await database.StringSetAsync(key, value, timeToLive);
        }

        public async Task<string?> GetCachedResponseAsync(string key)
        {
            var response = await database.StringGetAsync(key);
            return response.IsNull ? null : (string?)response;
        }
    }
}
