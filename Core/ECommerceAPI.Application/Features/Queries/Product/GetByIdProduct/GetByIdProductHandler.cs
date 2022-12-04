using ECommerceAPI.Application.Abstraction.Services.Product;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductHandler : IRequestHandler<GetByIdProductRequest, GetByIdProductResponse>
    {
        readonly IProductService _productService;

        public GetByIdProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<GetByIdProductResponse> Handle(GetByIdProductRequest request, CancellationToken cancellationToken)
        {
            var data = await _productService.GetProductByIdAsync(request.Id);
            return new()
            {
                Id = data.Id,
                Name = data.Name,
                Category = data.Category,
                Price = data.Price,
                Stock = data.Stock

            };
        }
    }
}
