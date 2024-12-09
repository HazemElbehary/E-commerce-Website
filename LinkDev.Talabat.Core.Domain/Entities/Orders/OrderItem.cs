using LinkDev.Talabat.Core.Domain.Common;

namespace LinkDev.Talabat.Core.Domain.Entities.Orders
{
	public class OrderItem : BaseAuditableEntity<int>
	{
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
