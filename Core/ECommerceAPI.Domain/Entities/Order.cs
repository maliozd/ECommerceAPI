﻿using ECommerceAPI.Domain.Entities.BasketEntities;
using ECommerceAPI.Domain.Entities.Common;

namespace ECommerceAPI.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string OrderCode { get; set; }
        public string Description { get; set; }
        public string Address { get; set; } //DDD
        public Basket Basket { get; set; }
        public CompletedOrder CompletedOrder { get; set; }
    }
}
