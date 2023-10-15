using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Features.Commands.Role.CreateRole;
using ECommerceAPI.Application.Features.Commands.Role.DeleteRole;
using ECommerceAPI.Application.Features.Commands.Role.UpdateRole;
using ECommerceAPI.Application.Features.Queries.Role.GetRoleById;
using ECommerceAPI.Application.Features.Queries.Role.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class RolesController : ControllerBase
    {
        readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [AuthorizeDefinition(ActionType = Application.Enums.ActionType.Get, Definition = "GetRoles", Menu = "Roles")]
        public async Task<IActionResult> GetRoles([FromQuery] GetRolesQueryRequest getRolesQueryRequest)
        {
            var response = await _mediator.Send(getRolesQueryRequest);
            return Ok(response);

        }
        [HttpGet("{id}")]
        [AuthorizeDefinition(ActionType = Application.Enums.ActionType.Get, Definition = "GetRoleById", Menu = "Roles")]
        public async Task<IActionResult> GetRoles([FromRoute] GetRoleByIdQueryRequest getRoleByIdQueryRequest)
        {
            var response = await _mediator.Send(getRoleByIdQueryRequest);
            return Ok(response);
        }
        [HttpPost]
        [AuthorizeDefinition(ActionType = Application.Enums.ActionType.Post, Definition = "CrateRole", Menu = "Roles")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommandRequest createRoleCommandRequest)
        {
            var response = await _mediator.Send(createRoleCommandRequest);
            return Ok(response);

        }
        [HttpPut("{id}")]
        [AuthorizeDefinition(ActionType = Application.Enums.ActionType.Update, Definition = "UpdateRole", Menu = "Roles")]
        public async Task<IActionResult> UpdateRole([FromBody, FromRoute] UpdateRoleCommandRequest updateRoleCommandRequest)
        {
            var response = await _mediator.Send(updateRoleCommandRequest);
            return Ok();
        }
        [HttpDelete("{name}")]
        [AuthorizeDefinition(ActionType = Application.Enums.ActionType.Delete, Definition = "DeleteRole", Menu = "Roles")]
        public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommandRequest deleteRoleCommandRequest)
        {
            var response = await _mediator.Send(deleteRoleCommandRequest);
            return Ok(response);
        }
    }
}
