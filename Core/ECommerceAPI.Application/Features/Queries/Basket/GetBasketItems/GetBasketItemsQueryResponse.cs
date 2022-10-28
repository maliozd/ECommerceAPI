﻿namespace ECommerceAPI.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketItemsQueryResponse
    {
        public int BasketItemId { get; set; } //basketItemId 
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}