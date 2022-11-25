using ECommerceAPI.Application.Dtos.Category;

namespace ECommerceAPI.Application.Features.Queries.Category.GetAllCategories
{
    public class GetParentCategoriesQueryResponse
    {
        public ICollection<CategoryIdNameDto> Categories { get; set; }
    }
}