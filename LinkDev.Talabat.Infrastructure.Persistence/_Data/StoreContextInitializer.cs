using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers;
using LinkDev.Talabat.Core.Domain.Data;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Product;
using LinkDev.Talabat.Infrastructure.Persistence.Common;
using System.Text.Json;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
    public class StoreContextInitializer(StoreContext context) : DbInitializer(context), IStoreContextInitializer
	{
		public override async Task SeedAsync()
		{
			if (context.Brands.Count() == 0)
			{
				var Brands = File.ReadAllText("../LinkDev.Talabat.Infrastructure.Persistence/_Data/Seeds/brands.json");
				var BrandsList = JsonSerializer.Deserialize<List<ProductBrand>>(Brands);
				if (BrandsList?.Count > 0)
					await context.Brands.AddRangeAsync(BrandsList);
			}
			if (context.Categories.Count() == 0)
			{
				var Categories = File.ReadAllText("../LinkDev.Talabat.Infrastructure.Persistence/_Data/Seeds/categories.json");
				var CategoriesList = JsonSerializer.Deserialize<List<ProductCategory>>(Categories);
				if (CategoriesList?.Count > 0)
					await context.Categories.AddRangeAsync(CategoriesList);
			}
			if (context.products.Count() == 0)
			{
				var Products = File.ReadAllText("../LinkDev.Talabat.Infrastructure.Persistence/_Data/Seeds/products.json");
				var ProductsList = JsonSerializer.Deserialize<List<Product>>(Products);
				if (ProductsList?.Count > 0)
					await context.products.AddRangeAsync(ProductsList);
			}
			if (context.DeliveryMethods.Count() == 0)
			{
				var DeliveryMethods = File.ReadAllText("../LinkDev.Talabat.Infrastructure.Persistence/_Data/Seeds/DeliveryMethods.json");
				var DeliveryMethodsList = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethods);
				if (DeliveryMethodsList?.Count > 0)
					await context.DeliveryMethods.AddRangeAsync(DeliveryMethodsList);
			}


			await context.SaveChangesAsync();
		}
	}
}
