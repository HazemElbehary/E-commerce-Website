using LinkDev.Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure.Payment;
using LinkDev.Talabat.Shared.SharedDTOs.Basket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Payment
{
    public class PaymentController(IPaymentService paymentService): BaseApiController
	{
		[Authorize]
		[HttpPost("{basketId}")]
		public async Task<ActionResult<Task<CustomerBasketDto>>> CreateOrUpdatePaymentIntentAsync(string basketId)
		{
			var basket = await paymentService.CreateOrUpdatePaymentIntent(basketId);
			return Ok(basket);
		}


        [HttpPost("webhook")]
        public async Task<IActionResult> Index()
        {
            var requestBody = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            await paymentService.UpdateOrderPaymentStatus(requestBody, Request.Headers["Stripe-Signature"]!);
			return Ok();
		}
    }
}
