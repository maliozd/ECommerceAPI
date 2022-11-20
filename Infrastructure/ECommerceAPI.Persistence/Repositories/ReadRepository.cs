﻿using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly APIDbContext _context;
        public ReadRepository(APIDbContext context)
        {
            _context = context;
        }
       

        public DbSet<T> Table => _context.Set<T>();       



        public IQueryable<T> GetAll()
        {
            var data = Table.AsQueryable();
            return data;
        }
        public async Task<T> GetByIdAsync(string id)
        {
          var data = await Table.FindAsync(Guid.Parse(id));
            if (data != null)
                return data;
            throw new Exception("Not found");
        }      
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression)
        {
            var query = Table.AsQueryable();
            
            return await query.FirstOrDefaultAsync(expression);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression)
        {                        
            return Table.Where(expression); 
        }
    }
}
