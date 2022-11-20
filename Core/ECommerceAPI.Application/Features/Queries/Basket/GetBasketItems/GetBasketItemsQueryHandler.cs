using ECommerceAPI.Application.Abstraction.Services.Basket;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.ProductImageFileRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketItemsQueryHandler : IRequestHandler<GetBasketItemsQueryRequest, List<GetBasketItemsQueryResponse>>
    {
        readonly IBasketService _basketService;
        readonly IProductReadRepository _productReadRepository;
        readonly IProductImageFileReadRepository productImageFileReadRepository;

        public GetBasketItemsQueryHandler(IBasketService basketService, IProductReadRepository productReadRepository, IProductImageFileReadRepository productImageFileReadRepository)
        {
            _basketService = basketService;
            _productReadRepository = productReadRepository;
            this.productImageFileReadRepository = productImageFileReadRepository;
        }

        public async Task<List<GetBasketItemsQueryResponse>> Handle(GetBasketItemsQueryRequest request, CancellationToken cancellationToken)
        {
            var items = await _basketService.GetBasketItemsAsync();
            return items.Select(item => new GetBasketItemsQueryResponse
            {
                BasketItemId = item.Id.ToString(),
                Name = item.Product.Name,
                Price = item.Product.Price,
                Quantity = item.Quantity,
            }).ToList();
                       

        }
    }
}
