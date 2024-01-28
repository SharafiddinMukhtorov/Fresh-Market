using FreshMarket.UI.Models;

namespace FreshMarket.UI.Services.Products
{
    public interface IProductsService
    {
        public IEnumerable<ProductDto> GetProducts(int? pageNumber);
        public ProductDto? GetProduct(int id);
    }
}
