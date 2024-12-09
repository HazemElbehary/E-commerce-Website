using LinkDev.Talabat.Core.Domain.Common;

namespace LinkDev.Talabat.Core.Domain.Entities.Product
{
    public class Product : BaseAuditableEntity<int>
    {
        public required string Name { get; set; }
        public required string NormalizedName { get; set; }
        public required string Description { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }

        // Foreign Key
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }


        // Navigation Properties
        public virtual ProductBrand? Brand { get; set; }
        public virtual ProductCategory? Category { get; set; }
    }
}
