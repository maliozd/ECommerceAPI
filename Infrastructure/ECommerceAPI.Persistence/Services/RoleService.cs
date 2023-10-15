using ECommerceAPI.Application.Abstraction.Services.Role;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Persistence.Services
{
    public class RoleService : IRoleService
    {
        readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public Dictionary<string, string> GetAllRoles()
        {
            var rolesDict = _roleManager.Roles.ToDictionary(role => role.Id, role => role.Name);
            return rolesDict;
        }
        public async Task<(string id, string name)> GetRoleById(string id)
        {
            string role = await _roleManager.GetRoleIdAsync(new() { Id = id });
            return (id, role);
        }
        public async Task<bool> CreateRoleAsync(string name)
        {
            IdentityResult result = await _roleManager.CreateAsync(new() { Name = name });
            return result.Succeeded;
        }
        public async Task<bool> DeleteRoleAsync(string name)
        {
            IdentityResult result = await _roleManager.DeleteAsync(new() { Name = name });
            return result.Succeeded;
        }
        public async Task<bool> UpdateRoleAsync(string id, string name)
        {
            IdentityResult result = await _roleManager.UpdateAsync(new() { Name = name, Id = id });
            return result.Succeeded;
        }

    }
}
