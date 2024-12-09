namespace LinkDev.Talabat.Core.Application.Abstraction.DTOs.Product
{
	public class CreateProductDto
	{
		public required string Name { get; set; }
		public required string Description { get; set; }
		public string? PictureUrl { get; set; }
		public decimal Price { get; set; }

		public int? BrandId { get; set; }
		public int? CategoryId { get; set; }
	}
}
