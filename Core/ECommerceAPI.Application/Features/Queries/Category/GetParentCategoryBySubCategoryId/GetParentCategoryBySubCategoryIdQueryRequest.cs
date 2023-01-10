using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Category.GetParentCategoryBySubCategoryId
{
    public class GetParentCategoryBySubCategoryIdQueryRequest : IRequest<GetParentCategoryBySubCategoryIdQueryResponse>
    {
        public string SubCategoryId { get; set; }
    }
}