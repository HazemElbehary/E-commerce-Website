using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Order;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Orders
{

	[Authorize]
	public class OrdersController(IServiceManager serviceManager): BaseApiController
	{
		[HttpPost("CreateOrder")]
		public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderToCreateDto dto)
		{
			var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
			var result = await serviceManager.OrderServie.CreateOrderAsync(BuyerEmail!, dto);

			return Ok(result);
		}

		[HttpGet] // GET: api/Orders
		public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetOrdersForUser()
		{
			var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);

			var result = await serviceManager.OrderServie.GetOrdersForUser(BuyerEmail!);

			return Ok(result);
		}

		[HttpGet("{id}")] // GET: api/Orders/{id}
		public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetOrder(int id)
		{
			var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);

			var result = await serviceManager.OrderServie.GetOrderByIdAsync(BuyerEmail!, id);

			return Ok(result);
		}

		[HttpGet("deliveryMethods")] // GET: api/Orders/deliveryMethods
		public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
		{
			var result = await serviceManager.OrderServie.GetDeliveryMethodsAsync();

			return Ok(result);
		}
	}
}
