﻿using ECommerceAPI.Application.Abstraction.Services.Role;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Role.GetRoles
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQueryRequest, GetRolesQueryResponse>
    {
        readonly IRoleService _roleService;

        public GetRolesQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<GetRolesQueryResponse> Handle(GetRolesQueryRequest request, CancellationToken cancellationToken)
        {
            var data = _roleService.GetAllRoles();
            return new()
            {
                Data = data
            };
        }
    }
}
