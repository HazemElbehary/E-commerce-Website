using LinkDev.Talabat.Core.Domain.Common;

namespace LinkDev.Talabat.Core.Domain.Contracts.Persistence.GenericRepository
{
    public interface IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false);
        Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> spec, bool withTracking = false);

        Task<TEntity?> GetAsync(TKey Id);

        Task<TEntity?> GetWithSpecAsync(ISpecifications<TEntity, TKey> spec);

        Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec);

        Task AddAsync(TEntity entity);

        void Updated(TEntity entity);

        void Delete(TEntity entity);
    }
}
