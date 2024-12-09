using LinkDev.Talabat.Core.Domain.Common;

namespace LinkDev.Talabat.Core.Domain.Entities.Orders
{
	public class AddressOfOrder : BaseEntity<int>
	{
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
    }
}
