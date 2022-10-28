using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.BasketEntities;

namespace ECommerceAPI.Application.Abstraction.Repositories.BasketItemRepository
{
    public interface IBasketItemWriteRepository : IWriteRepository<BasketItem>
    {
    }
}
