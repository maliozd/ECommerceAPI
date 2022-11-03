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

        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest)
        {
            var response = await _mediator.Send(createOrderCommandRequest);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrdersQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOrderById([FromRoute]GetByIdOrderQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] DeleteOrderByIdCommandRequest deleteByIdProductCommandRequest)
        {
            var response = await _mediator.Send(deleteByIdProductCommandRequest);
            return Ok(response);
        }
    }
}
