using ECommerceAPI.Application.Abstraction.Services.Role;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Role.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommandRequest, CreateRoleCommandResponse>
    {
        readonly IRoleService _roleService;

        public CreateRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
        {
            bool result = await _roleService.CreateRoleAsync(request.Name);
            return new(result);
        }
    }
}
