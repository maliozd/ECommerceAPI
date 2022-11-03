using ECommerceAPI.Application.Abstraction.Hubs;
using ECommerceAPI.Application.Abstraction.Services.Basket;
using ECommerceAPI.Application.Abstraction.Services.Order;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        readonly IOrderService _orderService;
        readonly IBasketService _basketService;
        readonly IOrderHubService _orderHubService;

        public CreateOrderCommandHandler(IOrderService orderService, IHttpContextAccessor httpContextAccessor, IBasketService basketService, IOrderHubService orderHubService)
        {
            _orderService = orderService;
            _basketService = basketService;
            _orderHubService = orderHubService;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await _orderService.CreateOrderAsync(new()
            {
                Address = request.Address,
                Description = request.Description,
                BasketId = _basketService.UserActiveBasket.Id
            });
            await _orderHubService.OrderCreatedMessageAsync("New order created!");
            return new();
        }
    }
}
