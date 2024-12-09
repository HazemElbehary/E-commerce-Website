using LinkDev.Talabat.Shared.SharedDTOs.Basket;

namespace LinkDev.Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure.Payment
{
    public interface IPaymentService
    {
        Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(string BasketId);
        Task UpdateOrderPaymentStatus(string requestBody, string Stripe_Signature);
    }
}
