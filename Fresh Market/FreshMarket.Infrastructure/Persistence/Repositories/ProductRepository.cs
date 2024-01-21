using FreshMarket.Domain.Entities;
using FreshMarket.Domain.Interfaces.Repositories;

namespace FreshMarket.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(FreshMarketDbContext context)
            : base(context)
        {
        }
    }
}
