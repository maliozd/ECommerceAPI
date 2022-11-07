﻿using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstraction.Repositories.CompletedOrderRepository
{
    public interface ICompletedOrderWriteRepository : IWriteRepository<CompletedOrder>
    {
    }
}