using ECommerceAPI.Application.Repositories.ProductImageFileRepository;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.FileEntities;
using ECommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories.Concrete
{
    public class ProductImageFileReadRepository : ReadRepository<ProductImageFile>, IProductImageFileReadRepository
    {
        public ProductImageFileReadRepository(APIDbContext context) : base(context)
        {
        }
    }
}
