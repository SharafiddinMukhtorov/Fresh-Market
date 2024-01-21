using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshMarket.Domain.ResourceParameters
{
    public class SupplyResourceParameters : ResourceParametersBase
    {
        public DateTime? DateTime { get; set; }
        public int? SupplierId { get; set; }
        public string? OrderBy { get; set; }
    }
}
