using ECommerceAPI.Application.Dtos.Category;

namespace ECommerceAPI.Application.Abstraction.Services.Category
{
    public interface ICategoryService
    {
        Task<PagedCategoriesDto> GetAllCategoriesPagedAsync(int page, int size);
        Task<bool> AddCategoryAsync();
        Task<List<CategoryIdNameDto>> GetParentCategoriesAsync();
        Task<List<CategoryIdNameDto>> GetChildCategoriesByParentIdAsync(string parentCategoryId);
        //Task<CategoryIdNameDto> GetParentCategoryBySubCategoryIdAsync(string subCategoryId);
        Task<SingleCategoryDto> GetDetailedCategoryByIdAsync(string categoryId);
        Task<CategoryIdNameDto> GetCategoryIdNameByIdAsync(string categoryId);
    }
}
