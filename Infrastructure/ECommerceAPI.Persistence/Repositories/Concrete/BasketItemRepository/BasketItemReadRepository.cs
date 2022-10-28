using ECommerceAPI.Application.Abstraction.Repositories.BasketItemRepository;
using ECommerceAPI.Domain.Entities.BasketEntities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Concrete.BasketItemRepository
{
    public class BasketItemReadRepository : ReadRepository<BasketItem>, IBasketItemReadRepository
    {
        public BasketItemReadRepository(APIDbContext context) : base(context)
        {
        }
    }
}
