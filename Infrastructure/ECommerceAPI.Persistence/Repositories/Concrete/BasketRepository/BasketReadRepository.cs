using ECommerceAPI.Application.Abstraction.Repositories.BasketRepository;
using ECommerceAPI.Domain.Entities.BasketEntities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Concrete.BasketRepository
{
    public class BasketReadRepository : ReadRepository<Basket>, IBasketReadRepository
    {
        public BasketReadRepository(APIDbContext context) : base(context)
        {
        }
    }
}
