using LinkDev.Talabat.Shared.SharedDTOs.Basket;

namespace LinkDev.Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure.Basket
{
    public interface IBasketService
    {
        Task<CustomerBasketDto> GetCustomerBasketAsync(string Id);
        Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto Basket);
        Task DeleteCustomerBasketAsync(string Id);
    }
}
