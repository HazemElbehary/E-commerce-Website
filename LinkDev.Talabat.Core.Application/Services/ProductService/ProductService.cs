using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Common;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Product;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Product;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Entities.Product;
using LinkDev.Talabat.Core.Domain.NIUnitOfWork;
using LinkDev.Talabat.Core.Domain.Specifications.Products;
using Microsoft.AspNetCore.Http;

namespace LinkDev.Talabat.Core.Application.Services.ProductServiceNS
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext) : IProductService
	{
		public async Task<Paginations<ProductToReturnDto>> GetProductsAsync(string? sort, int? brandId, int? categoryId, int pageSize, int pageIndex, string? search)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(sort, brandId, categoryId, pageSize, pageIndex, search);

			var Products = await unitOfWork.GetRepository<Product, int>().GetAllWithSpecAsync(spec);

			var SpecCount = new ProductWithFilterationForCountSpecifications(brandId, categoryId, search);

			var PrdCount = await unitOfWork.GetRepository<Product, int>().GetCountAsync(SpecCount);

			var Data = mapper.Map<IEnumerable<ProductToReturnDto>>(Products);

			return new Paginations<ProductToReturnDto>(pageIndex, pageSize, PrdCount, Data);
		}

		public async Task<ProductToReturnDto> GetProductAsync(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(id);


			var Product = await unitOfWork.GetRepository<Product, int>().GetWithSpecAsync(spec);

			if (Product is null)
				throw new _NotFoundException(nameof(Product), id);

			return mapper.Map<ProductToReturnDto>(Product);
		}

		public async Task<IEnumerable<CategoryToReturnDto>> GetCategoriesAsync()
		{
			var Categories = await unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();

			return mapper.Map<IEnumerable<CategoryToReturnDto>>(Categories);
		}

		public async Task<IEnumerable<BrandToReturnDto>> GetBrandsAsync()
		{
			var Brnads = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();

			return mapper.Map<IEnumerable<BrandToReturnDto>>(Brnads);
		}

		public async Task AddProductAsync(CreateProductDto ProductDto)
		{
			var product = new Product()
			{
				Name = ProductDto.Name,
				Description = ProductDto.Description,
				Price = ProductDto.Price,
				BrandId = ProductDto.BrandId,
				CategoryId = ProductDto.CategoryId,
				PictureUrl = ProductDto.PictureUrl,
				CreatedBy = httpContext?.HttpContext?.User.Claims.FirstOrDefault()?.Issuer ?? "1",
				CreatedOn = DateTime.UtcNow,
				NormalizedName = ProductDto.Name.ToUpper()
			};

			await unitOfWork.GetRepository<Product, int>().AddAsync(product);
			await unitOfWork.CompleteAsync();
		}
	
		public async Task DeleteBrand(int id)
		{
			var brand = await unitOfWork.GetRepository<ProductBrand, int>().GetAsync(id);
			unitOfWork.GetRepository<ProductBrand, int>().Delete(brand);
			await unitOfWork.CompleteAsync();

		}

		public async Task UpdateProduct(UpdateProductDto product)
		{
			var Getproduct = await unitOfWork.GetRepository<Product, int>().GetAsync(product.Id);
			unitOfWork.GetRepository<Product, int>().Updated(Getproduct);
			await unitOfWork.CompleteAsync();

		}

		public async Task DeleteProduct(int id)
		{
			var product = await unitOfWork.GetRepository<Product, int>().GetAsync(id);
			unitOfWork.GetRepository<Product, int>().Delete(product);
			await unitOfWork.CompleteAsync();
		}
	}
}
