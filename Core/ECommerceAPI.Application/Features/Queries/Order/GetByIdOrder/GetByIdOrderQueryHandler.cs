using ECommerceAPI.Application.Abstraction.Services.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Order.GetByIdOrder
{
    public class GetByIdOrderQueryHandler : IRequestHandler<GetByIdOrderQueryRequest, GetByIdOrderQueryResponse>
    {
        readonly IOrderService _orderService;

        public GetByIdOrderQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<GetByIdOrderQueryResponse> Handle(GetByIdOrderQueryRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetOrderByIdAsync(request.Id);
            return new()
            {
                Id = order.Id,
                Address = order.Address,
                BasketItems = order.BasketItems,
                CreatedDate = order.CreatedDate,
                Description = order.Description,
                OrderCode = order.OrderCode,
            };

        }
    }
}
