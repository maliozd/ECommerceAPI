using ECommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Application.Repositories.InvoiceFileRepository;

namespace ECommerceAPI.Persistence.Repositories.Concrete
{
    public class InvoiceFileReadRepository : ReadRepository<InvoiceFile>, IInvoiceFileReadRepository
    {
        public InvoiceFileReadRepository(APIDbContext context) : base(context)
        {
        }
    }
}
