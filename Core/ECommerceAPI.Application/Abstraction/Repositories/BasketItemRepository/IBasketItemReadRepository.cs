using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.BasketEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstraction.Repositories.BasketItemRepository
{
    public interface IBasketItemReadRepository : IReadRepository<BasketItem>
    {
    }
}
