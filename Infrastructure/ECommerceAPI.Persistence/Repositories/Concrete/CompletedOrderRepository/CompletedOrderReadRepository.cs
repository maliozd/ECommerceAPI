﻿using ECommerceAPI.Application.Abstraction.Repositories.CompletedOrderRepository;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories.Concrete.CompletedOrderRepository
{
    public class CompletedOrderReadRepository : ReadRepository<CompletedOrder>, ICompletedOrderReadRepository
    {

        public CompletedOrderReadRepository(APIDbContext context) : base(context)
        {

        }
    }
}
