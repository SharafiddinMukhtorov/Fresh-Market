using FreshMarket.Domain.DTOs.Product;
using FreshMarket.Domain.Responses;
using FreshMarket.Pagination.PaginatedList;
using FreshMarket.ResourceParameters;

namespace FreshMarket.Domain.Interfaces.Services
{
    public interface IProductService
    {
        GetProductsResponse GetProducts(ProductResourceParameters productResourceParameters);
        ProductDto? GetProductById(int id);
        ProductDto CreateProduct(ProductForCreateDto productToCreate);
        void UpdateProduct(ProductForUpdateDto productToUpdate);
        void DeleteProduct(int id);
    }
}
