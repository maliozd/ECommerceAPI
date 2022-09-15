﻿using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories.Concrete.ProductRepository
{
    public class ProductReadRepository :  ReadRepository<Product>, IProductReadRepository // --> interface ayrıştırıcı nesne 
    {
        public ProductReadRepository(APIDbContext context) : base(context)
        {
        }
    }
}
