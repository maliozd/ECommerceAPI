using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Category.GetAllCategories
{
    public class GetParentCategoriesQueryRequest : IRequest<GetParentCategoriesQueryResponse>
    {
    }
}