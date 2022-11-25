using ECommerceAPI.Application.Abstraction.Services.Category;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Category.GetSubCategories
{
    public class GetSubCategoryByParentIdQueryHandler : IRequestHandler<GetSubCategoryByParentIdQueryRequest, GetSubCategoryByParentIdQueryResponse>
    {
        readonly ICategoryService _categoryService;

        public GetSubCategoryByParentIdQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<GetSubCategoryByParentIdQueryResponse> Handle(GetSubCategoryByParentIdQueryRequest request, CancellationToken cancellationToken)
        {
            var childCategories = await _categoryService.GetSubCategoriesByParentIdAsync(request.ParentId);
            return new GetSubCategoryByParentIdQueryResponse
            {
                ChildCategories = childCategories,
            };
        }
    }
}
