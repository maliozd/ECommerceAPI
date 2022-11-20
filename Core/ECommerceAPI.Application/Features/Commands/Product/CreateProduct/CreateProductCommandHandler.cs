using ECommerceAPI.Application.Abstraction.Services.Product;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IProductService _productService;

        public CreateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var serviceResponse = await _productService.CreateProductAsync(new()
            {
                
                Price = request.Price,
                Stock = request.Stock,
                Name = request.Name,
            });
            return new()
            {
                Success = serviceResponse == true ? true : false,
            };
        }
    }
}
