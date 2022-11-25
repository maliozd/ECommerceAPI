using ECommerceAPI.Application.Dtos.Category;

namespace ECommerceAPI.Application.Abstraction.Services.Category
{
    public interface ICategoryService
    {
        Task<bool> AddCategoryAsync();
        Task<List<CategoryIdNameDto>> GetMainCategoriesAsync();
        Task<List<CategoryIdNameDto>> GetSubCategoriesByParentIdAsync(string parentCategoryId);

        Task<SingleCategoryDto> GetCategoryByIdAsync(string categoryId);
    }
}
