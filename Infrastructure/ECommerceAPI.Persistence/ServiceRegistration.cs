using ECommerceAPI.Application.Abstraction.Repositories.BasketItemRepository;
using ECommerceAPI.Application.Abstraction.Repositories.BasketRepository;
using ECommerceAPI.Application.Abstraction.Repositories.CategoryRepository;
using ECommerceAPI.Application.Abstraction.Repositories.CompletedOrderRepository;
using ECommerceAPI.Application.Abstraction.Services.Authentication;
using ECommerceAPI.Application.Abstraction.Services.Basket;
using ECommerceAPI.Application.Abstraction.Services.Category;
using ECommerceAPI.Application.Abstraction.Services.Order;
using ECommerceAPI.Application.Abstraction.Services.Product;
using ECommerceAPI.Application.Abstraction.Services.User;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.CustomerRepository;
using ECommerceAPI.Application.Repositories.InvoiceFileRepository;
using ECommerceAPI.Application.Repositories.ProductImageFileRepository;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Persistence.Contexts;
using ECommerceAPI.Persistence.Repositories.Concrete;
using ECommerceAPI.Persistence.Repositories.Concrete.BasketItemRepository;
using ECommerceAPI.Persistence.Repositories.Concrete.BasketRepository;
using ECommerceAPI.Persistence.Repositories.Concrete.CategoryRepository;
using ECommerceAPI.Persistence.Repositories.Concrete.CompletedOrderRepository;
using ECommerceAPI.Persistence.Repositories.Concrete.ProductRepository;
using ECommerceAPI.Persistence.Services;
using ECommerceAPI.Persistence.Services.Authentication;
using ECommerceAPI.Persistence.Services.Category;
using ECommerceAPI.Persistence.Services.Order;
using ECommerceAPI.Persistence.Services.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<APIDbContext>(options => options.UseSqlServer(Configuration.connectionString));

            #region Identity
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<APIDbContext>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            #endregion

            #region 

            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            _ = services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();

            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();

            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();

            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();

            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();

            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();

            services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
            services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();

            services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
            services.AddScoped<IBasketReadRepository, BasketReadRepository>();

            services.AddScoped<ICompletedOrderReadRepository, CompletedOrderReadRepository>();
            services.AddScoped<ICompletedOrderWriteRepository, CompletedOrderWriteRepository>();

            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
            #endregion

            #region Entity Service

            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            #endregion
        }
    }
}


//AddScoped() --> her request(web) için tek tek nesne gönderir. kullanıldıktan sonra nesneyi dispose eder. Task olmayan fonksiyonlarda await olmadığı için ve işlemden sonra kendini dispose ettiği için patlatır.
//AddSingleton() --> uygulamada tek bir nesne olacak, dispose edilmeyecek.sağlıksızdır?.
