using LinkDev.Talabat.APIs.Controllers.CacheAttribute;
using LinkDev.Talabat.Core.Application.Abstraction.Common;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Product;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Product
{
    public class ProductsController(IServiceManager serviceManager) : BaseApiController
    {
        [CachedAttribute(600)]
        [HttpGet]
        public async Task<ActionResult<Paginations<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams SpecParams)
        {
            var Products = await serviceManager.ProductService.GetProductsAsync(SpecParams.sort, SpecParams.brandId, SpecParams.categoryId, SpecParams.pageSize, SpecParams.pageIndex, SpecParams.Search);
            return Ok(Products);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var Product = await serviceManager.ProductService.GetProductAsync(id);


            return Ok(Product);
        }


        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandToReturnDto>>> GetBrands()
        {
            var Brands = await serviceManager.ProductService.GetBrandsAsync();
            return Ok(Brands);
        }


        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryToReturnDto>>> GetCategories()
        {
            var Categories = await serviceManager.ProductService.GetCategoriesAsync();
            return Ok(Categories);
        }

    }
}
