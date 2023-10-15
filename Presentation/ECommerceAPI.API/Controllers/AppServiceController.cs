using ECommerceAPI.Application.Abstraction.Services.Configurations;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class AppServiceController : ControllerBase
    {
        readonly IApplicationService _applicationService;

        public AppServiceController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }


        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Get, Definition = "GetAuthorizeDefinitonEndpoints")]
        public async Task<IActionResult> GetAuthorizeDefinitionEndpoints()
        {
            var data = _applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program));
            return Ok(data);
        }
    }
}
