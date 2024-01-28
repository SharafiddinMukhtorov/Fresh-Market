using FreshMarket.UI.Models;
using Newtonsoft.Json;

namespace FreshMarket.UI.Services.Products
{
    public class ProductsService : IProductsService
    {
        private readonly string url = "products";
        private readonly ApiClient _client = new ApiClient();

        public IEnumerable<ProductDto> GetProducts(int? pageNumber)
        {
            try
            {
                var response = _client.Get(url + $"?pageNumber={pageNumber ?? 1}") ?? throw new Exception("Response returned null.");

                var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var result = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(json);

                return result ?? Enumerable.Empty<ProductDto>();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public ProductDto? GetProduct(int id)
        {
            try
            {
                var response = _client.Get(url + "/" + id) ?? throw new Exception("Product with id: {id} not found.");
                

                var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var result = JsonConvert.DeserializeObject<ProductDto>(json);

                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
