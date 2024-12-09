namespace LinkDev.Talabat.Core.Application.Abstraction.DTOs.Order
{
	public class OrderToReturnDto
	{
        public int Id { get; set; }
		public required string BuyerEmail { get; set; }
		public DateTime OrderDate { get; set; }
		public required string OrderStatus { get; set; }
		public required AddressDto ShippingAddress { get; set; }
		public int? DeliveryMethodId { get; set; }
		public DeliveryMethodDto? DeliveryMethod { get; set; }
		public  required ICollection<OrderItemDto> Items { get; set; }
		public string? PaymentIntentId { get; set; }
		public decimal SubTotal { get; set; }
		public decimal Total { get; set; }
	}
}
