using ECommerceAPI.Application.Dtos.Configuration;

namespace ECommerceAPI.Application.Abstraction.Services.Configurations
{
    public interface IApplicationService
    {
        List<Menu> GetAuthorizeDefinitionEndpoints(Type type);
    }
}
