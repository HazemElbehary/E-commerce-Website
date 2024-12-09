using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.Talabat.Infrastructure.Persistence._Data.Configurations.Orders
{
	internal class DeliveryMethodConfigurations : BaseConfigurations<DeliveryMethod, int>
	{
		public override void Configure(EntityTypeBuilder<DeliveryMethod> builder)
		{
			base.Configure(builder);
			builder.Property(M => M.Cost).HasColumnType("decimal(8, 2)");
		}
	}
}
