using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Category.GetAllCategories
{
    public class GetCategoryByIdQueryRequest : IRequest<GetCategoryByIdQueryResponse>
    {
        public string CategoryId { get; set; }
    }
}