using LinkDev.Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure.Caching;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Orders;
using LinkDev.Talabat.Core.Application.MappingProfile;
using LinkDev.Talabat.Core.Application.Services;
using LinkDev.Talabat.Core.Application.Services.Auth;
using LinkDev.Talabat.Core.Application.Services.Basket;
using LinkDev.Talabat.Core.Application.Services.Orders;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Core.Application.DepaendancyInjection
{
    public static class DependancyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection service)
		{
			service.AddAutoMapper(typeof(MapProfile));
			service.AddScoped(typeof(IServiceManager), typeof(ServiceManager));


			service.AddScoped(typeof(IBasketService), typeof(BasketService));


			service.AddScoped(typeof(IOrderService), typeof(OrderServie));
			service.AddScoped(typeof(Func<IOrderService>), (serviceProvider) =>
			{
				return () => serviceProvider.GetRequiredService<IOrderService>();
			});

			service.AddScoped(typeof(IAuthService), typeof(AuthService));
			service.AddScoped(typeof(Func<IAuthService>), (serviceProvider) =>
            {
				return () => serviceProvider.GetRequiredService<IAuthService>();
			});

			return service;
		}
	}
}
