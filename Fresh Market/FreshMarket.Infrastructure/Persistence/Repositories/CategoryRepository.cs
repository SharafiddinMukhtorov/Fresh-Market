using FreshMarket.Domain.Entities;
using FreshMarket.Domain.Interfaces.Repositories;

namespace FreshMarket.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(FreshMarketDbContext context) : base(context)
        {
        }
    }
}
