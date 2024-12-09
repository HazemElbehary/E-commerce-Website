using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.Infrastructure.Persistence.Common
{
	public abstract class DbInitializer(DbContext _dbContext)
	{
		public async Task InitializeAsync()
		{
			var Migrations = await _dbContext.Database.GetPendingMigrationsAsync();

			if (Migrations.Any())
				await _dbContext.Database.MigrateAsync();
		}

		public abstract Task SeedAsync();
	}
}
