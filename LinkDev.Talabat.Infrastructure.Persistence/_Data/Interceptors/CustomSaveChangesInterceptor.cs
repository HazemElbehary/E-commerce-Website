using LinkDev.Talabat.Core.Application.Abstraction.LoggedInUserServices;
using LinkDev.Talabat.Core.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors
{
	public class CustomSaveChangesInterceptor(ILoggedInUserService loggedInUserService) : SaveChangesInterceptor
	{
		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			SetAuditeData(eventData.Context);
			return base.SavingChanges(eventData, result);
		}

		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			SetAuditeData(eventData.Context);
			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		private void SetAuditeData(DbContext dbContext)
		{
			if (dbContext is null)
				return;

			var entries = dbContext.ChangeTracker.Entries<IBaseAuditableEntity>()
				.Where(E => E.State is EntityState.Added or EntityState.Modified);

			foreach (var entry in entries)
			{
				var entity = entry.Entity;
				if (entry.State is EntityState.Added)
				{
					entity.CreatedBy = loggedInUserService.UserId ?? "1";
					entity.CreatedOn = DateTime.UtcNow;
				}

				entity.LastModifiedOn = DateTime.UtcNow;
				entity.LastModifiedBy = loggedInUserService.UserId ?? "1";
			}
		}
	}
}