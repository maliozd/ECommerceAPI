﻿using ECommerceAPI.Application.Dtos.Order;

namespace ECommerceAPI.Application.Features.Queries.Order
{
    public class GetAllOrdersQueryResponse
    {
        public int TotalOrderCount { get; set; }
        public object Orders { get; set; }
    }
}