using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Orders;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Product;
using LinkDev.Talabat.Core.Application.Services.ProductServiceNS;
using LinkDev.Talabat.Core.Domain.NIUnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Core.Application.Services
{
    internal class ServiceManager : IServiceManager
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;
		private readonly Lazy<IAuthService> _authService;
		private readonly Lazy<IProductService> _productService;
		private readonly Lazy<IOrderService> _orderService;

        public ServiceManager(
			IUnitOfWork unitOfWork,
			IMapper mapper, IConfiguration configuration,
			Func<IAuthService> AuthServiceFactry,
			Func<IOrderService> OrderServiceFactry,
			IHttpContextAccessor httpContext)
        {
			_mapper = mapper;
			_configuration = configuration;
			_unitOfWork = unitOfWork;
			_productService = new Lazy<IProductService>(()=> new ProductService(unitOfWork, mapper, httpContext));
			_authService = new Lazy<IAuthService>(AuthServiceFactry);
			_orderService = new Lazy<IOrderService>(OrderServiceFactry);
		}
        public IProductService ProductService => _productService.Value;

		public IAuthService AuthService => _authService.Value;

		public IOrderService OrderServie => _orderService.Value;
	}
}