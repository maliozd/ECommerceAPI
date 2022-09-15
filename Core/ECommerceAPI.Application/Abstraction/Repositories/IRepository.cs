using ECommerceAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories
{
    public interface IRepository<T>  where T : BaseEntity
    {
        //base repository olacak. global nesneleri tutacak.
        //Generic repository yapmak için generic nesneler kullanman lazım
        DbSet<T> Table { get; }
    }
}
