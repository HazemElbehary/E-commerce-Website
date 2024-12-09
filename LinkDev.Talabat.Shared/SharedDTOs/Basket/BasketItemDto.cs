using System.ComponentModel.DataAnnotations;

namespace LinkDev.Talabat.Shared.SharedDTOs.Basket
{
	public class BasketItemDto
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public required string ProductName { get; set; }
		public string? PictureUrl { get; set; }

		[Required]
		[Range(.1, double.MaxValue, ErrorMessage = "Price Must Be Greater Than Zero")]
		public decimal Price { get; set; }

		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Quantity Must Be Greater Than Zero")]
		public int Quantity { get; set; }

		public string? Brand { get; set; }
		public string? Category { get; set; }
	}
}
