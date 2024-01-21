using FreshMarket.Domain.DTOs.Product;

namespace FreshMarket.Domain.DTOs.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
       // public int NumberOfProducts { get; set; }
        public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
