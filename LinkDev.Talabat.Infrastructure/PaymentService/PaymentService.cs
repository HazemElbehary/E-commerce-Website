using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure.Payment;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure.BasketRepository;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.NIUnitOfWork;
using LinkDev.Talabat.Core.Domain.Specifications.Orders;
using LinkDev.Talabat.Shared.RedisConfigs;
using LinkDev.Talabat.Shared.SharedDTOs.Basket;
using LinkDev.Talabat.Shared.StripeConfgs;
using Microsoft.Extensions.Options;
using Stripe;
using Product = LinkDev.Talabat.Core.Domain.Entities.Product.Product;

namespace LinkDev.Talabat.Infrastructure.PaymentService
{
    internal class PaymentService(
        IBasketRepository basketRepository,
        IUnitOfWork unitOfWork,
        IOptions<RedisConfigurations> redisConfigs,
        IOptions<StripeSettings> stripeSettings,
        IMapper mapper
    )
        : IPaymentService
    {
        readonly RedisConfigurations RC = redisConfigs.Value;
        private readonly StripeSettings SS = stripeSettings.Value;

       
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(string BasketId)
        {
            StripeConfiguration.ApiKey = SS.ApiKey;

            // 1. Get Basket
            var basket = await basketRepository.GetAsync(BasketId);

            if (basket is null)
                throw new _NotFoundException(nameof(CustomerBasket), BasketId);

            // 2. Ensure Prices from Product Repository
            foreach (var item in basket.Items)
            {
                var productId = item.Id;
                var product = await unitOfWork.GetRepository<Product, int>().GetAsync(productId);

                item.Price = product!.Price;
            }

            // 3. Add Shipping Price ("Delivery Price")
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(basket.DeliveryMethodId);
            if (deliveryMethod is null) throw new _NotFoundException(nameof(DeliveryMethod), basket.DeliveryMethodId);
            basket.ShippingPrice = deliveryMethod!.Cost;

            // 4. Calculate Payment Amount
            var totalAmount = (long)(basket.ShippingPrice * 100 + basket.Items.Sum(item => item.Price * 100));

            // 5. Initialize PaymentIntentService
            PaymentIntentService paymentIntentService = new PaymentIntentService();

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                // Create new Payment Intent if it doesn't exist
                PaymentIntentCreateOptions paymentIntentCreateOptions = new()
                {
                    Amount = totalAmount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                var paymentIntent = await paymentIntentService.CreateAsync(paymentIntentCreateOptions);

                // Update basket with PaymentIntent details
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                // Update existing Payment Intent
                PaymentIntentUpdateOptions paymentIntentUpdateOptions = new()
                {
                    Amount = totalAmount
                };

                await paymentIntentService.UpdateAsync(basket.PaymentIntentId, paymentIntentUpdateOptions);
            }

            // 6. Update Basket in Repository
            await basketRepository.UpdateAsync(basket, RC.TimeToLive);

            // 7. Return Updated Basket

            return mapper.Map<CustomerBasketDto>(basket);
        }

        public async Task UpdateOrderPaymentStatus(string requestBody, string Stripe_Signature)
        {
            string endpointSecret = SS.SecretKey;

            var stripeEvent = EventUtility.ParseEvent(requestBody);

            stripeEvent = EventUtility.ConstructEvent(requestBody,
                    Stripe_Signature, endpointSecret);

            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                // Then define and call a method to handle the successful payment intent.
                await handlePaymentIntent(paymentIntent, true);
            }
            else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
            {
                // Then define and call a method to handle the successful attachment of a PaymentMethod.
                await handlePaymentIntent(paymentIntent, false);
            }
            else
            {
                throw new BadRequestException("UnExepected Payment Intent Status");
            }
        }

        private async Task handlePaymentIntent(PaymentIntent paymentIntent, bool IsSuccess)
        {
            var orderRepo = unitOfWork.GetRepository<Order, int>();
            var orderSpecs = new OrderSpecifications(paymentIntent.Id, true);
            var order = await orderRepo.GetWithSpecAsync(orderSpecs);

            if (order is null)
                throw new _NotFoundException(nameof(Order), $"PaymentId = {paymentIntent.Id}");

            if(IsSuccess)
                order.OrderStatus = OrderStatus.PaymentReceived;
            else
                order.OrderStatus = OrderStatus.PaymentFailed;
            orderRepo.Updated(order);
            await unitOfWork.CompleteAsync();
        }
    }
}
