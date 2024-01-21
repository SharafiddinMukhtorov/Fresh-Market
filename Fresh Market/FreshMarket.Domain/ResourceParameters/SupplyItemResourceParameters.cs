
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshMarket.Domain.ResourceParameters
{
    public class SupplyItemResourceParameters : ResourceParametersBase
    {
        public int? ProductId { get; set; }
        public int? SupplyId { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? UnitPriceLessThan { get; set; }
        public decimal? UnitPriceGreaterThan { get; set; }
        public int? Quantity { get; set; }
        public int? QuantityLessThan { get; set; }
        public int? QuantityGreaterThan { get; set; }
        public string? OrderBy { get; set; }
    }
}
