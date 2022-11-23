using ECommerceAPI.Application.Abstraction.Services.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class AppServiceController : ControllerBase
    {
        readonly IApplicationService _applicationService;

        public AppServiceController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = _applicationService.GetAuthorizeDefinitionEndpoint(typeof(Program));
            return Ok(data);
        }
    }
}
