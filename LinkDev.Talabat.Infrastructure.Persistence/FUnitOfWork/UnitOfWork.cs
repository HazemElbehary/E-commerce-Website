using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence.GenericRepository;
using LinkDev.Talabat.Core.Domain.Data;
using LinkDev.Talabat.Core.Domain.NIUnitOfWork;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories.GenericRepositories;
using System.Collections.Concurrent;

namespace LinkDev.Talabat.Infrastructure.Persistence.FUnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IAsyncDisposable
	{
		private readonly StoreContext _dbContext;
		ConcurrentDictionary<string, object> Repositories;

		public UnitOfWork(StoreContext context)
        {
			_dbContext = context;
			Repositories = new ();
		}

		public async Task<int> CompleteAsync()
		{
			return await _dbContext.SaveChangesAsync();
		}

		public async ValueTask DisposeAsync()
		{
			await _dbContext.DisposeAsync();
		}

		public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
			where TEntity : BaseEntity<TKey>
			where TKey : IEquatable<TKey>
		{
			return (GenericRepository<TEntity, TKey>)Repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(_dbContext));
		}
	}
}