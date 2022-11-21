using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories.Concrete
{
    public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
    {
        readonly APIDbContext _context;

        public OrderWriteRepository(APIDbContext sendContext) : base(sendContext) 
        {
            _context = sendContext;
        }
    }
   
    
}
