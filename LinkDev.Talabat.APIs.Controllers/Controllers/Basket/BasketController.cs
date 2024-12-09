using LinkDev.Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure.Basket;
using LinkDev.Talabat.Shared.SharedDTOs.Basket;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Basket
{
    public class BasketController(IBasketService basketService) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasket([FromQuery] string id) // GET: /api/basket?id="12"
        {
            var basket = await basketService.GetCustomerBasketAsync(id);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket([FromBody] CustomerBasketDto basketDto)
        {
            var basket = await basketService.UpdateCustomerBasketAsync(basketDto);
            return Ok(basket);
        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await basketService.DeleteCustomerBasketAsync(id);
        }
    }
}
