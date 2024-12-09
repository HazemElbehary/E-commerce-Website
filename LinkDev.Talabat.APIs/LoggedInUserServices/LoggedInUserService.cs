using LinkDev.Talabat.Core.Application.Abstraction.LoggedInUserServices;
using System.Security.Claims;

namespace LinkDev.Talabat.APIs.LoggedInUserServices
{
	public class LoggedInUserService : ILoggedInUserService
	{
        string? userId;
		public string? UserId => userId;

        public LoggedInUserService(IHttpContextAccessor? httpContextAccessor)
        {
            var httpContext = httpContextAccessor?.HttpContext;
            userId = httpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);
        }
    }
}
