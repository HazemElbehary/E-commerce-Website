using LinkDev.Talabat.Core.Domain.Entities.Product;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories.GenericRepositories;

namespace LinkDev.Talabat.Core.Domain.Specifications.Products
{
	public class ProductWithFilterationForCountSpecifications : BaseISpecifications<Product, int>
	{
        public ProductWithFilterationForCountSpecifications(int? brandId, int? categoryId, string? search) : 
            base(
					P =>
					(
						(string.IsNullOrEmpty(search) || (P.NormalizedName.Contains(search)))
								&&
						(!brandId.HasValue || P.BrandId == brandId)
							&&
						(!categoryId.HasValue || P.CategoryId == categoryId)
					)
				)
        {
        }
    }
}
