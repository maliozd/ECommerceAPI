using ECommerceAPI.Application.Abstraction.Services.Category;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Category.GetAllCategories
{
    public class GetParentCategoriesQueryHandler : IRequestHandler<GetParentCategoriesQueryRequest, GetParentCategoriesQueryResponse>
    {
        readonly ICategoryService _categoryService;

        public GetParentCategoriesQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<GetParentCategoriesQueryResponse> Handle(GetParentCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            var categories = await _categoryService.GetMainCategoriesAsync();
            return new()
            {
                Categories = categories,
            };
        }
    }
}
