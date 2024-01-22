using FreshMarket.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshMarket.Domain.Entities
{
    public class Customer : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
