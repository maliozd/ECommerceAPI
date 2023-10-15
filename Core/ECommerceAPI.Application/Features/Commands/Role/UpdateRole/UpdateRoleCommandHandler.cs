using ECommerceAPI.Application.Abstraction.Services.Role;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Role.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommandRequest, UpdateRoleCommandResponse>
    {
        readonly IRoleService _roleService;

        public UpdateRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken)
        {
            bool result = await _roleService.UpdateRoleAsync(request.Id, request.Name);
            return new(result);
        }
    }
}
