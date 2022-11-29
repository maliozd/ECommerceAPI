using ECommerceAPI.Application.Abstraction.Services.Category;
using ECommerceAPI.Application.Dtos.Category;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Category.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQueryRequest, GetAllCategoriesQueryResponse>
    {
        readonly ICategoryService _categoryService;

        public GetAllCategoriesQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<GetAllCategoriesQueryResponse> Handle(GetAllCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            PagedCategoriesDto categoryData = await _categoryService.GetAllCategoriesPagedAsync(request.Page, request.Size);

            return new()
            {
                TotalCount = categoryData.TotalCount,
                Categories = categoryData.Categories.OrderByDescending(c => c.IsParentCategory).ToList()
            };

        }
    }
}
