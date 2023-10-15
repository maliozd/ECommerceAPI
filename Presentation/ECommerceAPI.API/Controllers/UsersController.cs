using ECommerceAPI.Application.Abstraction.Services.Mail;
using ECommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using ECommerceAPI.Application.Features.Queries.User.GetUserInfo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly IMailService mailService;
        public UsersController(IMediator mediator, IMailService mailService)
        {
            _mediator = mediator;
            this.mailService = mailService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            var response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        //[UsernameBindActionFilter]
        public async Task<IActionResult> GetUserInfo(GetUserInfoQueryRequest getUserInfoQueryRequest)
        {
            getUserInfoQueryRequest.Username = Request.HttpContext.User.Identity.Name;
            var response = await _mediator.Send(getUserInfoQueryRequest);
            return Ok(response);
        }

    }
}
