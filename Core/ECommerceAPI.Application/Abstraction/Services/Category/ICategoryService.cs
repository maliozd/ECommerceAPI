using ECommerceAPI.Application.Dtos.Category;

namespace ECommerceAPI.Application.Abstraction.Services.Category
{
    public interface ICategoryService
    {
        Task<PagedCategoriesDto> GetAllCategoriesPagedAsync(int page, int size);
        Task<bool> AddCategoryAsync();
        Task<List<CategoryIdNameDto>> GetMainCategoriesAsync();
        Task<List<CategoryIdNameDto>> GetSubCategoriesByParentIdAsync(string parentCategoryId);

        Task<SingleCategoryDto> GetDetailedCategoryByIdAsync(string categoryId);
        Task<CategoryIdNameDto> GetCategoryIdNameByIdAsync(string categoryId);
    }
}
