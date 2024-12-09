using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.Talabat.Core.Domain.Data.Configurations
{
	internal class BaseAuditableConfigurations<TEntity, TKey> : BaseConfigurations<TEntity, TKey>
		where TEntity : BaseAuditableEntity<TKey>
		where TKey : IEquatable<TKey> 
		
		   
	{
		public override void Configure(EntityTypeBuilder<TEntity> builder)
		{
			base.Configure(builder);

			builder.Property(E => E.CreatedBy).IsRequired();

			builder.Property(E => E.CreatedOn).IsRequired();

		}
	}
}
