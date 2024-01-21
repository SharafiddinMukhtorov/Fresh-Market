using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshMarket.Domain.ResourceParameters
{
    public class SaleResourceParameters : ResourceParametersBase
    {
        public int? CustomerId { get; set; }

        public string? OrderBy { get; set; }
    }
}
