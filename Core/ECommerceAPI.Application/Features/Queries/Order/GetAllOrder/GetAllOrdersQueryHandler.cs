using ECommerceAPI.Application.Abstraction.Services.Order;
using ECommerceAPI.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Order
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, GetAllOrdersQueryResponse>
    {
        readonly IOrderService _orderService;

        public GetAllOrdersQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<GetAllOrdersQueryResponse> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetAllOrdersAsync(request.Page,request.Size);
            return new GetAllOrdersQueryResponse
            {
                Orders = orders.Orders,
                TotalOrderCount = orders.TotalOrderCount
            };
        }
    }
}