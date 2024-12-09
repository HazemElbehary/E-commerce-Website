using LinkDev.Talabat.Core.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Orders;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Product;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services
{
    public interface IServiceManager
	{
        public IProductService ProductService { get; }
        public IAuthService AuthService { get; }
        public IOrderService OrderServie { get; }
    }
}
