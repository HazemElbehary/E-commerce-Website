using LinkDev.Talabat.Core.Domain.Data.Configurations;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.Talabat.Infrastructure.Persistence._Data.Configurations.Orders
{
	internal class OrderConfigurations : BaseAuditableConfigurations<Order, int>
	{
		public override void Configure(EntityTypeBuilder<Order> builder)
		{
			base.Configure(builder);

			builder.OwnsOne(order => order.ShippingAddress, address => address.WithOwner());

			builder.Property(order => order.OrderStatus)
				.HasConversion
				(
					(OStatus) => OStatus.ToString(),
					(OStatus) => (OrderStatus) Enum.Parse(typeof(OrderStatus), OStatus)
				);

			builder.Property(order => order.SubTotal)
				.HasColumnType("decimal(8, 2)");

			builder.HasOne(order => order.DeliveryMethod)
				.WithMany()
				.HasForeignKey(order => order.DeliveryMethodId)
				.OnDelete(DeleteBehavior.SetNull);

			builder.HasMany(order => order.Items)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade);

		}
	}
}
