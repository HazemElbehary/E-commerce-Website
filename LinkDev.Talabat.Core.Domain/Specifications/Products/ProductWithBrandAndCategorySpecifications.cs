using LinkDev.Talabat.Core.Domain.Entities.Product;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories.GenericRepositories;

namespace LinkDev.Talabat.Core.Domain.Specifications.Products
{
	public class ProductWithBrandAndCategorySpecifications : BaseISpecifications<Product, int>
	{
        public ProductWithBrandAndCategorySpecifications(string? sort, int? brandId, int? categoryId, int PageSize, int PageIndex, string? search) 
			: base(
					P => 
					(
						(string.IsNullOrEmpty(search) || (P.NormalizedName.Contains(search)))
							&&
						(!brandId.HasValue || P.BrandId == brandId))
							&&
						(!categoryId.HasValue || P.CategoryId == categoryId)
				   )
		{
			AddIncludes();

			switch (sort)
				{
					case "nameDesc":
						AddOrderByDesc(p => p.Name);
						break;
					case "priceAsc":
						AddOrderBy(P => P.Price);
						break;
					case "priceDesc":
						AddOrderByDesc(P => P.Price);
						break;
					default:
						AddOrderBy(p => p.Name);
						break;
				}

			ApplyPagination(PageSize, PageIndex);
		}

		public ProductWithBrandAndCategorySpecifications(int id) : base(id)
        {
			Criteria = P => P.Id == id;
			AddIncludes();
		}

		#region Helpers

		private protected override void AddIncludes()
		{
			base.AddIncludes();

			Includes.Add(P => P.Brand!);
			Includes.Add(P => P.Category!);
		}



		#endregion
	}
}
