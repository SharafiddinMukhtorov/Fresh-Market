using FreshMarket.Domain.Common;

namespace FreshMarket.Domain.Entities
{
    public class Category : EntityBase
    {
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
