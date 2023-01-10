using ECommerceAPI.Application.Abstraction.Repositories.CategoryRepository;
using ECommerceAPI.Application.Abstraction.Services.Category;
using ECommerceAPI.Application.Dtos.Category;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Services.Category
{
    public class CategoryService : ICategoryService
    {
        readonly ICategoryReadRepository _categoryReadRepository;
        readonly ICategoryWriteRepository _categoryWriteRepository;

        public CategoryService(ICategoryWriteRepository categoryWriteRepository, ICategoryReadRepository categoryReadRepository)
        {
            _categoryWriteRepository = categoryWriteRepository;
            _categoryReadRepository = categoryReadRepository;
        }

        public Task<bool> AddCategoryAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedCategoriesDto> GetAllCategoriesPagedAsync(int page, int size)
        {
            var totalCount = _categoryReadRepository.GetAll().Count();
            var categories = await _categoryReadRepository.GetAll().Include(c => c.ParentCategory).Include(c => c.ChildCategories).Skip(page * size).Take(size).Select(c => new SingleCategoryDto
            {
                Id = c.Id.ToString(),
                Name = c.Name,
                ChildCategories = c.ChildCategories.Select(ch => ch.Name).ToList(),
                ParentCategory = c.ParentCategory.Name,
                IsParentCategory = c.ParentCategory == null ? true : false
            }).ToListAsync();

            return new()
            {
                TotalCount = totalCount,
                Categories = categories
            };
        }

        public async Task<CategoryIdNameDto> GetCategoryIdNameByIdAsync(string categoryId)
        {
            var category = await _categoryReadRepository.Table.Where(c => c.Id == Guid.Parse(categoryId)).Select(c => new CategoryIdNameDto
            {
                Id = c.Id.ToString(),
                Name = c.Name
            }).FirstOrDefaultAsync();
            return category;
        }

        public async Task<SingleCategoryDto> GetDetailedCategoryByIdAsync(string categoryId)
        {

            var _category = await _categoryReadRepository.Table.Where(c => c.Id == Guid.Parse(categoryId)).Include(c => c.ParentCategory).Include(c => c.ChildCategories).FirstOrDefaultAsync();
            SingleCategoryDto category = new();
            var childNames = new List<string>();
            if (_category?.ChildCategories?.Count > 0)
            {
                foreach (var child in _category.ChildCategories)
                    childNames.Add(child.Name);
                category.ChildCategories = childNames;
            }


            if (_category.ParentCategory != null)
                category.ParentCategory = _category.ParentCategory.Name;
            category.IsParentCategory = _category.ParentCategory == null ? true : false;
            category.Id = categoryId;
            category.Name = _category.Name;
            return category;
        }

        public async Task<List<CategoryIdNameDto>> GetParentCategoriesAsync()
        {
            var mainCategories = await _categoryReadRepository.GetAll().Where(c => c.ParentCategoryId == null).Select(c => new CategoryIdNameDto
            {
                Id = c.Id.ToString(),
                Name = c.Name
            }).ToListAsync();
            return mainCategories;
        }

        public async Task<List<CategoryIdNameDto>> GetChildCategoriesByParentIdAsync(string parentCategoryId)
        {
            var subCategories = await _categoryReadRepository.GetAll().Where(c => c.ParentCategoryId == Guid.Parse(parentCategoryId)).Select(c => new CategoryIdNameDto
            {
                Id = c.Id.ToString(),
                Name = c.Name
            }).ToListAsync();
            return subCategories;
        }

        //public async Task<CategoryIdNameDto> GetParentCategoryBySubCategoryIdAsync(string subCategoryId)
        //{
        //    var parentCategory = 
        //}
    }
}
