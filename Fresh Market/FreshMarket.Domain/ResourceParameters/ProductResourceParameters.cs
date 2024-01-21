using FreshMarket.Domain.ResourceParameters;

namespace FreshMarket.ResourceParameters
{
    public class ProductResourceParameters : ResourceParametersBase
    {        
        public int? CategoryId { get; set; }
        public string? SearchString { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceLessThan { get; set; }
        public decimal? PriceGreaterThan { get; set; }
    
        public string? OrderBy { get; set; }

    }
}
