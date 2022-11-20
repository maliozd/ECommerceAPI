using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.BasketEntities;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Entities.FileEntities;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Contexts
{
    public class APIDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public APIDbContext()
        {

        }
        public APIDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFile { get; set; }
        public DbSet<BaseFile> Files { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<CompletedOrder> CompletedOrders { get; set; }
        
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>().HasKey(b => b.Id);
            builder.Entity<Order>().HasIndex(o => o.OrderCode).
                IsUnique();

            builder.Entity<Order>()
                .HasOne(o => o.CompletedOrder)
                .WithOne(c => c.Order)
                .HasForeignKey<CompletedOrder>(c => c.OrderId);


            builder.Entity<Basket>().
                HasOne(b => b.Order).
                WithOne(o => o.Basket).
                HasForeignKey<Order>(b => b.Id);


            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // ChangeTracker -> entitylerde yapılan değişikliklerin veya yeni eklenen datanın yakalanmasını sağlar. propertydir. Track edilen verileri yakalar.
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var entity in datas)
            {
                _ = entity.State switch // _ -> create dummy variable. unassigned variable.
                {
                    EntityState.Added => entity.Entity.CreatedDate = DateTime.Now,
                    EntityState.Modified => entity.Entity.UpdatedDate = DateTime.Now,
                    _ => DateTime.Now
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
