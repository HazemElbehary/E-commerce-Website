namespace LinkDev.Talabat.Core.Application.Abstraction.DTOs.Order
{
	public class AddressDto
	{
        public int Id { get; set; }
        public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Street { get; set; }
		public required string City { get; set; }
		public required string Country { get; set; }
	}
}
