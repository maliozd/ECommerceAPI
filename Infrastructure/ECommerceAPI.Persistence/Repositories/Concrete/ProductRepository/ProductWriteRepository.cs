using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;
using System.Linq.Expressions;

namespace ECommerceAPI.Persistence.Repositories.Concrete.ProductRepository
{
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(APIDbContext context) : base(context)
        {

        }
      
    }
}
