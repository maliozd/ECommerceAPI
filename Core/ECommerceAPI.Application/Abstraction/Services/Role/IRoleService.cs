namespace ECommerceAPI.Application.Abstraction.Services.Role
{
    public interface IRoleService
    {
        Dictionary<string, string> GetAllRoles();
        Task<(string id, string name)> GetRoleById(string id);
        Task<bool> CreateRoleAsync(string name);
        Task<bool> DeleteRoleAsync(string name);
        Task<bool> UpdateRoleAsync(string id, string name);


    }
}
