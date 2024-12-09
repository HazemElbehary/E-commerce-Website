using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class InitializerExtensions
	{
		public async static Task<WebApplication> InitializeStoreContext(this WebApplication app)
		{
			using var scope = app.Services.CreateAsyncScope();
			var services = scope.ServiceProvider;

			var storeContextInitializer = services.GetRequiredService<IStoreContextInitializer>();
			var storeIdentityInitializer = services.GetRequiredService<IStoreIdentityDbInitializer>();
			var Logger = services.GetRequiredService<ILogger<Program>>();

			try
			{
				await storeContextInitializer.InitializeAsync();
				await storeContextInitializer.SeedAsync();
				await storeIdentityInitializer.InitializeAsync();
				await storeIdentityInitializer.SeedAsync();
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, "Sorry, An Error Occured");
			}

			return app;
		}
	}
}
