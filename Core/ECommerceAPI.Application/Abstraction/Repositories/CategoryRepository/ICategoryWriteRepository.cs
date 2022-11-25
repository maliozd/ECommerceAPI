using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Application.Abstraction.Repositories.CategoryRepository
{
    public interface ICategoryWriteRepository : IWriteRepository<Category>
    {
    }
}
