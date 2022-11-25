using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Category.GetSubCategories
{
    public class GetSubCategoryByParentIdQueryRequest : IRequest<GetSubCategoryByParentIdQueryResponse>
    {
        public string ParentId { get; set; }
    }
}