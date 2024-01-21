using FreshMarket.Domain.DTOs.Product;
using FreshMarket.Pagination.PaginatedList;
using FreshMarket.ResourceParameters;

namespace FreshMarket.Domain.Interfaces.Services
{
    public interface IProductService
    {
        PaginatedList<ProductDto> GetProducts(ProductResourceParameters productResourceParameters);
        ProductDto? GetProductById(int id);
        ProductDto CreateProduct(ProductForCreateDto productToCreate);
        void UpdateProduct(ProductForUpdateDto productToUpdate);
        void DeleteProduct(int id);
    }
}
