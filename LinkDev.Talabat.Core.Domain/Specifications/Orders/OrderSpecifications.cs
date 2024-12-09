using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories.GenericRepositories;

namespace LinkDev.Talabat.Core.Domain.Specifications.Orders
{
	public class OrderSpecifications: BaseISpecifications<Order, int>
	{
		public OrderSpecifications(string Buyeremail, int Id)
			: base(O => O.Id == Id && O.BuyerEmail == Buyeremail)
		{
			AddIncludes();
		}

		public OrderSpecifications(string Buyeremail) 
			:base(O => O.BuyerEmail == Buyeremail)
		      {
			AddIncludes();
			AddOrderByDesc(order => order.OrderDate);
		      }

		public OrderSpecifications(string PaymenrIntentId, bool isPayment)
			: base(O => O.PaymentIntentId == PaymenrIntentId)
		{
		}


		private protected override void AddIncludes()
		{
			base.AddIncludes();
			Includes.Add(O => O.DeliveryMethod!);
			Includes.Add(O => O.Items);
		}
	}
}
