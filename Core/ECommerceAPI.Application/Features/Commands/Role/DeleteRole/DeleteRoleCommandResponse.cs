namespace ECommerceAPI.Application.Features.Commands.Role.DeleteRole
{
    public class DeleteRoleCommandResponse
    {
        public bool Succeeded { get; set; }

        public DeleteRoleCommandResponse(bool succeeded)
        {
            Succeeded = succeeded;
        }
    }
}