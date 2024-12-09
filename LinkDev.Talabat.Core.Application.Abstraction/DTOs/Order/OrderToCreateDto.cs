namespace LinkDev.Talabat.Core.Application.Abstraction.DTOs.Order
{
	public class OrderToCreateDto
	{
        public required string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public required AddressDto shipToAddress { get; set; }
    }
}