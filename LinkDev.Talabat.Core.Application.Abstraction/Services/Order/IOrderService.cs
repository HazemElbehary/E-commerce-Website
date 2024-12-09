using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Order;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Orders
{
	public interface IOrderService
	{
		Task<OrderToReturnDto> CreateOrderAsync(string BuyerEmail, OrderToCreateDto createDto);
		Task<OrderToReturnDto> GetOrderByIdAsync(string BuyerEmail, int Id);
		Task<IEnumerable<OrderToReturnDto>> GetOrdersForUser(string BuyerEmail);
		Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();
	}
}
