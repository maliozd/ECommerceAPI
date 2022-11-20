using ECommerceAPI.Application.Abstraction.Services.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.User.GetUserInfo
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQueryRequest, GetUserInfoQueryResponse>
    {
        readonly IUserService _userService;

        public GetUserInfoQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetUserInfoQueryResponse> Handle(GetUserInfoQueryRequest request, CancellationToken cancellationToken)
        {
            var info = _userService.GetUserInfoAsync(request.Username).GetAwaiter().GetResult();
            return info;
        }
    }
}
