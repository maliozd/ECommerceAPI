﻿using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>    //dönecek responseu tanımlamamız lazım
    {
        //public Pagination Pagination { get; set; }
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
