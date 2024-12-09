using LinkDev.Talabat.Core.Domain.Data.Configurations;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.Talabat.Infrastructure.Persistence._Data.Configurations.Orders
{
	internal class OrderItemConfigurations : BaseAuditableConfigurations<OrderItem, int>
	{
		public override void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			base.Configure(builder);
			builder.Property(OItem => OItem.Price).HasColumnType("decimal(18,2)");
		}
	}
}
