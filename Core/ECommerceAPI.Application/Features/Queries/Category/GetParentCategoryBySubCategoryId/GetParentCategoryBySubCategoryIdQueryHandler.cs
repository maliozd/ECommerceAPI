using ECommerceAPI.Application.Abstraction.Services.Category;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Category.GetParentCategoryBySubCategoryId
{
    public class GetParentCategoryBySubCategoryIdQueryHandler : IRequestHandler<GetParentCategoryBySubCategoryIdQueryRequest, GetParentCategoryBySubCategoryIdQueryResponse>
    {
        readonly ICategoryService _categoryService;

        public GetParentCategoryBySubCategoryIdQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public Task<GetParentCategoryBySubCategoryIdQueryResponse> Handle(GetParentCategoryBySubCategoryIdQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
