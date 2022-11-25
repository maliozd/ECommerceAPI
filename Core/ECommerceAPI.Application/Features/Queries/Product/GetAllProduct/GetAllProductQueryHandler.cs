using ECommerceAPI.Application.Abstraction.Services.Product;
using ECommerceAPI.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly ILogger<GetAllProductQueryHandler> logger;
        readonly IProductService _productService;
        public GetAllProductQueryHandler(IProductReadRepository productReadRepository, ILogger<GetAllProductQueryHandler> logger, IProductService productService)
        {
            _productReadRepository = productReadRepository;
            this.logger = logger;
            _productService = productService;
        }
        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            this.logger.LogInformation("GetAllProduct log example.");
            var pagedProducts = await _productService.GetAllProductsPagedAsync(request.Page, request.Size);

            return new()
            {
                Products = pagedProducts.Products,
                TotalCount = pagedProducts.TotalCount,
            };

            //var totalCount = _productReadRepository.GetAll().Count();
            //var products = _productReadRepository.GetAll().Skip(request.Page * request.Size).Take(request.Size).Include(x => x.ProductImageFiles).Select(p => new
            //{
            //    p.Id,
            //    p.Name,
            //    p.Stock,
            //    p.Price,
            //    p.CreatedDate,
            //    p.UpdatedDate,
            //    p.ProductImageFiles
            //}).ToList();

            //return new()
            //{
            //    TotalCount = totalCount,
            //    Products = products
            //};
        }
    }
}
