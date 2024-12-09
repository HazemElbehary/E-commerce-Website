using LinkDev.Talabat.Core.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;

namespace LinkDev.Talabat.Core.Domain.Data.Configurations
{
    internal class BrandConfiguration : BaseAuditableConfigurations<ProductBrand, int>
	{
		public override void Configure(EntityTypeBuilder<ProductBrand> builder)
		{
			base.Configure(builder);

			builder.Property(B => B.Name).IsRequired();
			builder.HasIndex(B => B.Name).IsUnique();
		}
	}
}
