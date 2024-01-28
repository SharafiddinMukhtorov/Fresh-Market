using FreshMarket.UI.Models;

namespace FreshMarket.UI.VIewModels
{
    public class ProductsViewModel
    {
        public IEnumerable<ProductDto> Data { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public int PageNumber { get; set; }
    }
}
