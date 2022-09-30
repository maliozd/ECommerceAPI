using ECommerceAPI.Application.Repositories;
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
        public async Task<T> GetByIdAsync(int id/*,bool tracking*/)
        {
            //var data = Table.AsQueryable();
            //if (!tracking) Write işlemi olmadığı için, doğal olarak değişiklik de olmayacağı için veri trackingini kapatabiliriz. ekstra yükten kurtaran bir optimizasyon
            //{
            //    data.AsNoTracking();
            //}
            return await Table.FindAsync(id);
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression)
        {           
            return await Table.FindAsync(expression);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression)
        {                        
            return Table.Where(expression); 
        }
    }
}
