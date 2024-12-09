using LinkDev.Talabat.Core.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.Talabat.Core.Domain.Data.Configurations
{
    internal class ProductConfigurations : BaseAuditableConfigurations<Product, int>
	{
		public override void Configure(EntityTypeBuilder<Product> builder)
		{
			base.Configure(builder);

			builder.Property(P => P.Name).IsRequired().HasMaxLength(100);
			builder.Property(P => P.NormalizedName).IsRequired().HasMaxLength(100);
			builder.Property(P => P.Description).IsRequired();
			builder.Property(P => P.Price).HasColumnType("decimal(9, 2)");

			builder.HasOne(P => P.Brand)
				   .WithMany()
				   .HasForeignKey(P => P.BrandId)
				   .OnDelete(DeleteBehavior.SetNull);

			builder.HasOne(P => P.Category)
				   .WithMany()
				   .HasForeignKey(P => P.CategoryId)
				   .OnDelete(DeleteBehavior.SetNull);
		}
	}
}
