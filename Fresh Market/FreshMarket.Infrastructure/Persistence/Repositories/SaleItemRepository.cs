﻿using FreshMarket.Domain.Entities;
using FreshMarket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshMarket.Infrastructure.Persistence.Repositories
{
    public class SaleItemRepository : RepositoryBase<SaleItem>, ISaleItemRepository
    {
        public SaleItemRepository(FreshMarketDbContext context)
            : base(context)
        {
        }
    }
}
