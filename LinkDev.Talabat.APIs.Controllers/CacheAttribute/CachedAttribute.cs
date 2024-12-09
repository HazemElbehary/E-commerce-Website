using LinkDev.Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure.Caching;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace LinkDev.Talabat.APIs.Controllers.CacheAttribute
{
    internal class CachedAttribute(int timeToLive) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 1. Get cachedService
            var cachedService = context.HttpContext.RequestServices.GetRequiredService<IResponseCachingService>();
            
            // 2. Generate Key Base On Request
            var key = GenerateKey(context.HttpContext.Request);

            var response = await cachedService.GetCachedResponseAsync(key);

            // 3. Inject response in request if it's already cached
            if(response is not null)
            {
                var result = new ContentResult()
                {
                    Content = response,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = result;
                return;
            }

            // 4. Execute the end point if response is not cached
            var endPointResponse = await next.Invoke();

            // 5. Caches the result of end point
            if (endPointResponse.Result is OkObjectResult okObjectResult && okObjectResult is not null) 
            {
                await cachedService.CachResponseAsync(key, okObjectResult.Value, TimeSpan.FromSeconds(timeToLive));
            }
        }

        private string GenerateKey(HttpRequest request)
        {
            // Request Ex: {{url}}/api/products/pageinedx=1&pageSize=5&sort=name
            var key = new StringBuilder();

            // 1. Append: api/products
            key.Append(request.Path);

            // 2. Append Each Segment: |pageinedx-1|.....
            foreach (var (k, value) in request.Query.OrderBy(q => q.Key))
            {
                key.Append($"|{k}-{value}");
            }

            return key.ToString();
        }
    }
}
