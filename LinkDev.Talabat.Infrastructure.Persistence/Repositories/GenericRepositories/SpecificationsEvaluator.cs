using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories.GenericRepositories
{
    public static class SpecificationsEvaluator<TEntity, TKey> 
		where TEntity : BaseEntity<TKey> 
		where TKey : IEquatable<TKey>
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> query, ISpecifications<TEntity, TKey> spec)
		{
			if (spec.Criteria is not null)
				query = query.Where(spec.Criteria);



			if (spec.OrderByDesc is not null)
				query = query.OrderByDescending(spec.OrderByDesc);
			else if (spec.OrderBy is not null)
				query = query.OrderBy(spec.OrderBy);

			if (spec.IsPaginate)
				query = query.Skip(spec.Skip).Take(spec.Take);

			foreach (var Include in spec.Includes)
            {
				query.Include(Include);
            }

			return query;
		}
	}
}
