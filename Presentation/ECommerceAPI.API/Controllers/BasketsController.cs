using ECommerceAPI.Application.Constants;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Enums;
using ECommerceAPI.Application.Features.Commands.Basket.AddItemToBasket;
using ECommerceAPI.Application.Features.Commands.Basket.RemoveBasketItem;
using ECommerceAPI.Application.Features.Commands.Basket.UpdateQuantity;
using ECommerceAPI.Application.Features.Queries.Basket.GetBasketItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")] //status code 401 --> unauthorized

    public class BasketsController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly IHttpContextAccessor httpContextAccessor;


        public BasketsController(IMediator mediator, ILogger<BasketsController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Baskets,ActionType = ActionType.Get,Definiton = "GetBasketItems")]
        public async Task<IActionResult> GetBasketItems([FromQuery]GetBasketItemsQueryRequest request)
        {
            List<GetBasketItemsQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Baskets, ActionType = ActionType.Post, Definiton = "AddItemToBasket")]

        public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        [AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Baskets, ActionType = ActionType.Update, Definiton = "UpdateQuantity")]

        public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("{BasketItemId}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Baskets, ActionType = ActionType.Delete, Definiton = "RemoveBasketItem")]

        public async Task<IActionResult> RemoveBasketItem([FromRoute] RemoveBasketItemCommandRequest request)
        {
            RemoveBasketItemCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
