using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure.BasketRepository;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using StackExchange.Redis;
using System.Text.Json;

namespace LinkDev.Talabat.Infrastructure.Repository
{
    internal class BasketRepository : IBasketRepository
	{
		private IDatabase _database;

		public BasketRepository(IConnectionMultiplexer connectionMultiplexer)
        {
			_database = connectionMultiplexer.GetDatabase();
		}

        public async Task<CustomerBasket?> GetAsync(string id)
		{
			var Basket = await _database.StringGetAsync(id);


			var basket = Basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(Basket!);
			return basket;

		}

		public async Task<CustomerBasket?> UpdateAsync(CustomerBasket basket, TimeSpan TimeToLive)
		{
			var Basket = await _database
				.StringSetAsync(
					basket.Id,
					JsonSerializer.Serialize(basket),
					TimeToLive
				);

			return basket;
		}
		
		public async Task<bool> DeleteAsync(string id)
		{
			return await _database.KeyDeleteAsync(id);
		}
	}
}
