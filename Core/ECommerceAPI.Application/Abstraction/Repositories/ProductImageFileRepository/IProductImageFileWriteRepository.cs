﻿using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.FileEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories.ProductImageFileRepository
{
    public interface IProductImageFileWriteRepository : IWriteRepository<ProductImageFile>
    {
    }
}
