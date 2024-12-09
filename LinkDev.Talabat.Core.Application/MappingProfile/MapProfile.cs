using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Order;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Product;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Product;
using LinkDev.Talabat.Shared.SharedDTOs.Basket;

namespace LinkDev.Talabat.Core.Application.MappingProfile
{
	public class MapProfile : Profile
	{
        public MapProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Brand, O => O.MapFrom(src => src.Brand!.Name))
				.ForMember(d => d.Category, O => O.MapFrom(src => src.Category!.Name))
				.ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());

            CreateMap<ProductCategory, CategoryToReturnDto>();
            CreateMap<ProductBrand, BrandToReturnDto>();

            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dis => dis.PaymentIntentId, Src => Src.MapFrom(src => src.PaymentIntentId));
            
            
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(
                    ODto => ODto.PictureUrl,
                    Options => Options.MapFrom<OrderItemPictureUrlResolver>()
                );

            CreateMap<AddressOfOrder, AddressDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<DeliveryMethod, DeliveryMethodDto>();
            CreateMap<CustomerBasket, CustomerBasketDto>();
		}
    }
}
