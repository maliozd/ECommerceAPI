using ECommerceAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly ILogger<GetAllProductQueryHandler> logger;
        public GetAllProductQueryHandler(IProductReadRepository productReadRepository, ILogger<GetAllProductQueryHandler> logger)
        {
            _productReadRepository = productReadRepository;
            this.logger = logger;
        }
        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            this.logger.LogInformation("Ürünler listelendi.");
            var totalCount = _productReadRepository.GetAll().Count();
            var products = _productReadRepository.GetAll().Skip(request.Page * request.Size).Take(request.Size).Include(x => x.ProductImageFiles).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate,
                p.ProductImageFiles
            }).ToList();

            return new()
            {
                TotalCount = totalCount,
                Products = products
            };
        }
    }
}
