namespace LinkDev.Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure.Caching
{
    public interface IResponseCachingService
    {
        Task CachResponseAsync(string key, object response, TimeSpan timeToLive);
        Task<string?> GetCachedResponseAsync(string key);
    }
}
