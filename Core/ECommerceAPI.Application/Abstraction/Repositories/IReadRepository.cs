using ECommerceAPI.Domain.Entities.Common;
using System.Linq.Expressions;

namespace ECommerceAPI.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();   //Listten farklı olarak, yazmış olduğumuz kodsal sorgular dbye giden query ile eklenir/gider. List ise önce veriyi memorye çeker, ondan sonra kod içerisinde sorgu yapar.
        IQueryable<T> GetWhere(Expression<Func<T, bool>> expression);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(Guid id);
    }
}
