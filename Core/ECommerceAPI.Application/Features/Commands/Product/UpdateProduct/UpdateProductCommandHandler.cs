using ECommerceAPI.Application.Abstraction.Services.Product;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly IProductService _productService;

        public UpdateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var isUpdated = await _productService.UpdateProductAsync(new Dtos.Product.UpdateProductDto
            {
                Id = request.Id,
                CategoryId = request.CategoryId,
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
            });
            return new()
            {
                Success = isUpdated
            };
        }
    }
}
