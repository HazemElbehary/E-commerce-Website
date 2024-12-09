using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure.Basket;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure.BasketRepository;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using LinkDev.Talabat.Shared.SharedDTOs.Basket;
using Microsoft.Extensions.Configuration;
namespace LinkDev.Talabat.Core.Application.Services.Basket
{
    internal class BasketService : IBasketService
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;

		public BasketService(IBasketRepository basketRepository, IMapper mapper, IConfiguration configuration)
        {
			_basketRepository = basketRepository;
			_mapper = mapper;
			_configuration = configuration;
		}

        public async Task<CustomerBasketDto> GetCustomerBasketAsync(string Id)
		{
			var basket = await _basketRepository.GetAsync(Id);

			if (basket is not null)
				return _mapper.Map<CustomerBasketDto>(basket);

			throw new _NotFoundException(nameof(CustomerBasket), Id);
		}

		public async Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto basketDto)
		{
			var basket = _mapper.Map<CustomerBasket>(basketDto);
			
			var timeToLive = TimeSpan.FromDays(double.Parse(_configuration.GetSection("RedisSettings")["TimeToLive"]!));

			var updatedBasket = await _basketRepository.UpdateAsync(basket, timeToLive);

			if (updatedBasket is null) throw new BadRequestException("Can not Update Basket :(");

			return basketDto;
		}
		
		public async Task DeleteCustomerBasketAsync(string Id)
		{
			var deleted = await _basketRepository.DeleteAsync(Id);

			if(!deleted) throw new BadRequestException("unable to deleted this basket");
		}
	}
}
