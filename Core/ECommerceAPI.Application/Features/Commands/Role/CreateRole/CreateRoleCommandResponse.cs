namespace ECommerceAPI.Application.Features.Commands.Role.CreateRole
{
    public class CreateRoleCommandResponse
    {
        public bool Succeeded { get; set; }

        public CreateRoleCommandResponse(bool succeded)
        {
            Succeeded = succeded;
        }
    }
}