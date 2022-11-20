using MediatR;

namespace ECommerceAPI.Application.Features.Queries.User.GetUserInfo
{
    public class GetUserInfoQueryRequest : IRequest<GetUserInfoQueryResponse>
    {
        public string? Username;
    }
}