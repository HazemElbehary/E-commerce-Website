using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Product;
using LinkDev.Talabat.Core.Domain.Entities.Product;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Core.Application.MappingProfile
{
	public class ProductPictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductToReturnDto, string?>
	{
		public string? Resolve(Product source, ProductToReturnDto destination, string? destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureUrl))
				return $"{configuration["Urls:ApiBaseUrl"]}/{source.PictureUrl}";

			return string.Empty;
		}
	}
}
