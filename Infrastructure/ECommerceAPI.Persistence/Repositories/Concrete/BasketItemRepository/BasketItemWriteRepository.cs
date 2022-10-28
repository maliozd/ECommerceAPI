using ECommerceAPI.Application.Abstraction.Repositories.BasketItemRepository;
using ECommerceAPI.Domain.Entities.BasketEntities;
using ECommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories.Concrete.BasketItemRepository
{
    public class BasketItemWriteRepository : WriteRepository<BasketItem>, IBasketItemWriteRepository
    {
        public BasketItemWriteRepository(APIDbContext context) : base(context)
        {
        }
    }
}
