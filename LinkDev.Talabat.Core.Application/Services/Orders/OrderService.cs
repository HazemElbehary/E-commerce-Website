using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure.Payment;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Order;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Orders;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Product;
using LinkDev.Talabat.Core.Domain.NIUnitOfWork;
using LinkDev.Talabat.Core.Domain.Specifications.Orders;

namespace LinkDev.Talabat.Core.Application.Services.Orders
{
    internal class OrderServie(IMapper mapper, IBasketService basketService, IUnitOfWork unitOfWork, IPaymentService paymentService) : IOrderService
	{
		public async Task<OrderToReturnDto> CreateOrderAsync(string BuyerEmail, OrderToCreateDto OrderToCreateDto)
		{
			// 1. Get Basket from Basket repo
			var basket = await basketService.GetCustomerBasketAsync(OrderToCreateDto.BasketId);

			// 2. Get Selected Items At Basket From Product repo
			var OrderItems = new List<OrderItem>();

			if (basket.Items.Count() > 0)
			{
				var productRepo = unitOfWork.GetRepository<Product, int>();

				foreach (var item in basket.Items)
				{
					var product = await productRepo.GetAsync(item.Id);
					
					if (product != null)
					{
						var orderItem = new OrderItem() 
						{
							ProductId = product.Id,
							ProductName = product.Name,
							PictureUrl = product.PictureUrl!,
							Price = product.Price,
							Quantity = item.Quantity
						};
					
						OrderItems.Add(orderItem);
					}
				}

			}

			if (OrderItems.Count() == 0)
				throw new _NotFoundException("Products", "Ids");

			// 3. Calculate SubTotal
			decimal SubTotal = OrderItems.Sum(OI => OI.Quantity * OI.Price);

			// 4. Mapp Address
			var address = mapper.Map<AddressOfOrder>(OrderToCreateDto.shipToAddress);

			// 5. Get Delivery Method
			var DeliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(OrderToCreateDto.DeliveryMethodId);

			// check if order is already exists
			var OrderSpecs = new OrderSpecifications(basket.PaymentIntentId!, true);
			var OrderExist = await unitOfWork.GetRepository<Order, int>().GetWithSpecAsync(OrderSpecs);

			if(OrderExist is not null)
			{
				unitOfWork.GetRepository<Order, int>().Delete(OrderExist);
				await paymentService.CreateOrUpdatePaymentIntent(basket.Id);
			}


			// 6. Create Order
			var orderToCreate = new Order() 
			{
				BuyerEmail = BuyerEmail,
				ShippingAddress = address,
				SubTotal = SubTotal,
				DeliveryMethod = DeliveryMethod,
				Items = OrderItems,
				PaymentIntentId = basket.PaymentIntentId.ToString()
			};


			await unitOfWork.GetRepository<Order, int>().AddAsync(orderToCreate);

			// 7. Save To DataBase
			var Created = await unitOfWork.CompleteAsync() > 0;

			if (!Created) throw new BadRequestException("An Error Has Occured During Creating The Order");
		
			var res =  mapper.Map<OrderToReturnDto>(orderToCreate);
			return res;
		}

		public async Task<IEnumerable<OrderToReturnDto>> GetOrdersForUser(string BuyerEmail)
		{
			var Specs = new OrderSpecifications(BuyerEmail);
			var orders = await unitOfWork.GetRepository<Order, int>().GetAllWithSpecAsync(Specs);
			return mapper.Map<IEnumerable<OrderToReturnDto>>(orders);
		}


		public async Task<OrderToReturnDto> GetOrderByIdAsync(string BuyerEmail, int Id)
		{
			var Specs = new OrderSpecifications(BuyerEmail, Id);
			var order = await unitOfWork.GetRepository<Order, int>().GetWithSpecAsync(Specs);
			if (order is null) throw new _NotFoundException(nameof(Order), Id);
			
			return mapper.Map<OrderToReturnDto>(order);
		}

		public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
		{
			var DeliveryMethods =  await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();

			return mapper.Map<IEnumerable<DeliveryMethodDto>>(DeliveryMethods);
		}
	}
}
