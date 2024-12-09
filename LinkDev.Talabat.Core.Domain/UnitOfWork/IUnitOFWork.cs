using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence.GenericRepository;

namespace LinkDev.Talabat.Core.Domain.NIUnitOfWork
{
    public interface IUnitOfWork
	{
		IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() 
			where TEntity : BaseEntity<TKey> 
			where TKey : IEquatable<TKey>;
		Task<int> CompleteAsync();
	}
}
