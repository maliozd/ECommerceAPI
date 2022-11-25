using ECommerceAPI.Application.Dtos.Category;

namespace ECommerceAPI.Application.Features.Queries.Category.GetAllCategories
{
    public class GetCategoryByIdQueryResponse
    {
        public SingleCategoryDto Category { get; set; }
    }
}