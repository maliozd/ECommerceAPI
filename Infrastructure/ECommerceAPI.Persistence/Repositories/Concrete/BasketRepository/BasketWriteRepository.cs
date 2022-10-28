using ECommerceAPI.Application.Abstraction.Repositories.BasketRepository;
using ECommerceAPI.Domain.Entities.BasketEntities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Concrete.BasketRepository
{
    public class BasketWriteRepository : WriteRepository<Basket>, IBasketWriteRepository
    {
        public BasketWriteRepository(APIDbContext context) : base(context)
        {
        }
    }
}
