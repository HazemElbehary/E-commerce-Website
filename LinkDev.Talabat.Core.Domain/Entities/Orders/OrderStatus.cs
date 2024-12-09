using System.Runtime.Serialization;

namespace LinkDev.Talabat.Core.Domain.Entities.Orders
{
	public enum OrderStatus
	{
		Pending = 1,
		[EnumMember(Value = "Payment Received")]
		PaymentReceived,
		[EnumMember(Value = "Payment Failed")]
		PaymentFailed
	}
}
