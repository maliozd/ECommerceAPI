using ECommerceAPI.Application.Constants;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Enums;
using ECommerceAPI.Application.Features.Commands.Order.CompleteOrder;
using ECommerceAPI.Application.Features.Commands.Order.CreateOrder;
using ECommerceAPI.Application.Features.Commands.Order.DeleteOrder;
using ECommerceAPI.Application.Features.Commands.Product.DeleteProduct;
using ECommerceAPI.Application.Features.Queries.Order;
using ECommerceAPI.Application.Features.Queries.Order.GetByIdOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OrdersController : ControllerBase
    {
        readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")] //status code 401 --> unauthorized
        [AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Orders, ActionType = ActionType.Post,Definiton = "CreateOrder")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Orders, ActionType = ActionType.Get, Definiton = "GetAllOrders")]

        public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrdersQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Orders, ActionType = ActionType.Get, Definiton = "GetOrderById")]

        public async Task<IActionResult> GetOrderById([FromRoute] GetByIdOrderQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Orders, ActionType = ActionType.Delete, Definiton = "DeleteOrder")]

        public async Task<IActionResult> DeleteOrder([FromRoute] DeleteOrderByIdCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("complete-order/{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Orders, ActionType = ActionType.Update, Definiton = "CompleteOrder")]

        public async Task<IActionResult> CompleteOrder([FromRoute] CompleteOrderCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
