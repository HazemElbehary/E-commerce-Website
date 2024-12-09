using LinkDev.Talabat.Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LinkDev.Talabat.Infrastructure.Persistence.Common;
using LinkDev.Talabat.Core.Domain.Data;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Configurations
{
	[DbContextTypeAttribute(typeof(StoreContext))]
	internal class BaseConfigurations<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>

	{
		public virtual void Configure(EntityTypeBuilder<TEntity> builder)
		{
			builder.Property(E => E.Id).ValueGeneratedOnAdd();
		}
	}
}
