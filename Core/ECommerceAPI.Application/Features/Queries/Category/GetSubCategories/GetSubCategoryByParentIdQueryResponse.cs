using ECommerceAPI.Application.Dtos.Category;

namespace ECommerceAPI.Application.Features.Queries.Category.GetSubCategories
{
    public class GetSubCategoryByParentIdQueryResponse
    {
        public ICollection<CategoryIdNameDto> ChildCategories { get; set; }
    }
}