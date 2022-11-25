using ECommerceAPI.Application.Abstraction.Repositories.CategoryRepository;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Concrete.CategoryRepository
{
    public class CategoryReadRepository : ReadRepository<Category>, ICategoryReadRepository
    {
        public CategoryReadRepository(APIDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
