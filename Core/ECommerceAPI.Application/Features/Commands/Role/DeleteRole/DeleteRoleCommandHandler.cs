﻿using ECommerceAPI.Application.Abstraction.Services.Role;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Role.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommandRequest, DeleteRoleCommandResponse>
    {
        readonly IRoleService _roleService;

        public DeleteRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken)
        {
            bool result = await _roleService.DeleteRoleAsync(request.Name);
            return new(result);
        }
    }
}
