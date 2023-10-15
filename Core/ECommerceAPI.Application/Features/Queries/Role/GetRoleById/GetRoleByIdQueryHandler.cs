using ECommerceAPI.Application.Abstraction.Services.Role;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Role.GetRoleById
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQueryRequest, GetRoleByIdQueryResponse>
    {
        readonly IRoleService _roleService;

        public GetRoleByIdQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<GetRoleByIdQueryResponse> Handle(GetRoleByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var roleTupleData = await _roleService.GetRoleById(request.Id);
            return new(roleTupleData.id, roleTupleData.name);
        }
    }
}
