using LinkDev.Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure.Caching;
using LinkDev.Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure.Payment;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure.BasketRepository;
using LinkDev.Talabat.Infrastructure.Caching;
using LinkDev.Talabat.Infrastructure.Repository;
using LinkDev.Talabat.Shared.RedisConfigs;
using LinkDev.Talabat.Shared.StripeConfgs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace LinkDev.Talabat.Infrastructure
{
    public static class DependancyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
		{
			services.AddSingleton<IConnectionMultiplexer>( (serviceProvider) =>
			{
				var connectionString = config.GetConnectionString("RedisConnection");

				return ConnectionMultiplexer.Connect(connectionString!);
			});


			services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
			services.AddScoped(typeof(IPaymentService), typeof(PaymentService.PaymentService));
            services.AddSingleton(typeof(IResponseCachingService), typeof(ResponseCachingService));

            services.Configure< RedisConfigurations>(config.GetSection("RedisSettings"));
			services.Configure<StripeSettings>(config.GetSection("StripeSettings"));

			return services;
		}
    }
}
