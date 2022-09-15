using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        readonly private APIDbContext _context;
        public WriteRepository(APIDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();
        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }
        public async Task<bool> AddRangeAsync(List<T> entity)
        {
            //foreach (T entry in entity)
            //{
            //    EntityEntry<T> entityEntry = await Table.AddAsync(entry);
            //    entityEntry.State = EntityState.Added;
            //}
            await Table.AddRangeAsync(entity);
            return true;
        }
        public bool Remove(T entity)
        {
            EntityEntry<T> entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }
        public async Task<bool> Remove(int id)
        {
            T modelToDelete = await Table.Where(x => x.Id == id).FirstOrDefaultAsync();
            return Remove(modelToDelete);
        }
        public bool RemoveRange(List<T> entity)
        {
            Table.RemoveRange(entity);
            return true;
        }
        public bool Update(T entity)
        {
            EntityEntry entityEntry = Table.Update(entity);  //ilgili veri context üzerinden gelmiyorsa yani tracking edilmiyorsa kullanılır. ??
            //await SaveAsync();
            return entityEntry.State == EntityState.Modified;
        }
        public Task<int> SaveAsync()
        => _context.SaveChangesAsync();
    }
}
