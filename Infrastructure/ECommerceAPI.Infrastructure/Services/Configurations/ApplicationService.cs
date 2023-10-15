using ECommerceAPI.Application.Abstraction.Services.Configurations;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Dtos.Configuration;
using ECommerceAPI.Application.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace ECommerceAPI.Infrastructure.Services.Configurations
{
    public class ApplicationService : IApplicationService
    {
        /// <summary>
        /// /// 
        /// </summary>
        /// <param name="type">
        /// incoming type -> typeof(Program)
        /// </param>
        /// <returns></returns>
        public List<Menu> GetAuthorizeDefinitionEndpoints(Type type)
        {
            Assembly? assembly = Assembly.GetAssembly(type);
            var controllers = assembly?.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));
            List<Menu> definitionEndpointsMenu = new();

            foreach (Type controller in controllers)
            {
                var actionsMethodInfos = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));
                if (actionsMethodInfos != null)
                {
                    foreach (MethodInfo action in actionsMethodInfos)
                    {
                        var attributes = action.GetCustomAttributes(true);
                        if (attributes != null)
                            AddActionsToEndpointsMenu(attributes, definitionEndpointsMenu);
                    }
                }
            }
            return definitionEndpointsMenu;
        }
        private void AddActionsToEndpointsMenu(object[] attributes, List<Menu> definitionEndpointsMenu)
        {
            Menu menu = new();
            var authorizeDefAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;

            if (!definitionEndpointsMenu.Any(m => m.MenuName == authorizeDefAttribute.Menu))
            {
                menu = new() { MenuName = authorizeDefAttribute.Menu };
                definitionEndpointsMenu.Add(menu);
            }
            else
                menu = definitionEndpointsMenu.FirstOrDefault(m => m.MenuName == authorizeDefAttribute.Menu);

            Application.Dtos.Configuration.Action customAction = new()
            {
                ActionType = Enum.GetName(typeof(ActionType), authorizeDefAttribute.ActionType),
                Definition = authorizeDefAttribute.Definition,
            };

            HttpMethodAttribute? httpAttribute = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;

            if (httpAttribute != null)
                customAction.HttpType = httpAttribute.HttpMethods.First();
            else
                customAction.HttpType = HttpMethods.Get;

            customAction.UniqueCode = $"{customAction.HttpType}-{customAction.ActionType}-{customAction.Definition}";
            menu.Actions.Add(customAction);
        }
    }
}
