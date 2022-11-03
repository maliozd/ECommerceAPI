using ECommerceAPI.Application.Abstraction.Services.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Order.DeleteOrder
{
    public class DeleteOrderByIdCommandHandler : IRequestHandler<DeleteOrderByIdCommandRequest, DeleteOrderByIdCommandResponse>
    {
        readonly IOrderService _orderService;

        public DeleteOrderByIdCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<DeleteOrderByIdCommandResponse> Handle(DeleteOrderByIdCommandRequest request, CancellationToken cancellationToken)
        {
            await _orderService.DeleteOrderAsync(request.Id);
            return new();
        }
    }
}
