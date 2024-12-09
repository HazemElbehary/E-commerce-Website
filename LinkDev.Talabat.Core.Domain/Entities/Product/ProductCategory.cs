using LinkDev.Talabat.Core.Domain.Common;

namespace LinkDev.Talabat.Core.Domain.Entities.Product
{
    public class ProductCategory : BaseAuditableEntity<int>
    {
        public required string Name { get; set; }
    }
}
