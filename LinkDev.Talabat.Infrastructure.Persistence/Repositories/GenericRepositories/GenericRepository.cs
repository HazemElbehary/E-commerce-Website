using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence.GenericRepository;
using LinkDev.Talabat.Core.Domain.Data;
using LinkDev.Talabat.Core.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories.GenericRepositories
{
    public class GenericRepository<TEntity, TKey>(StoreContext dbContext) : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false)
        {
			if (typeof(TEntity) == typeof(Product))
			{
			    if (withTracking)
			        return (IEnumerable<TEntity>)await dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).ToListAsync();
			
			    return (IEnumerable<TEntity>)await dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).AsNoTracking().ToListAsync();
			}
			
			if (withTracking)
			    return await dbContext.Set<TEntity>().ToListAsync();
			
			return await dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
		}

		public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> spec ,bool withTracking = false)
		{
			return await SpecificationsEvaluator<TEntity, TKey>.GetQuery(dbContext.Set<TEntity>(), spec).ToListAsync();
        }

		public async Task<TEntity?> GetAsync(TKey Id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return await dbContext.Set<Product>().Where(P => P.Id.Equals(Id)).Include(P => P.Brand).Include(P => P.Category).FirstOrDefaultAsync() as TEntity;
            }
            
            return await dbContext.Set<TEntity>().FindAsync(Id);

		}
		public async Task<TEntity?> GetWithSpecAsync(ISpecifications<TEntity, TKey> spec)
		{
			return await SpecificationsEvaluator<TEntity, TKey>.GetQuery(dbContext.Set<TEntity>(), spec).FirstOrDefaultAsync();
		}
		
        public async Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec)
        {
			return await SpecificationsEvaluator<TEntity, TKey>.GetQuery(dbContext.Set<TEntity>(), spec).CountAsync();
		}

		public async Task AddAsync(TEntity entity)
        {
            await dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Updated(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

    }
}
