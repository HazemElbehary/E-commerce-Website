using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Order;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Core.Application.MappingProfile
{
	public class OrderItemPictureUrlResolver(IConfiguration configuration) : IValueResolver<OrderItem, OrderItemDto, string>
	{
		public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureUrl))
				return $"{configuration["Urls:ApiBaseUrl"]}/{source.PictureUrl}";

			return string.Empty;
		}
	}
}
