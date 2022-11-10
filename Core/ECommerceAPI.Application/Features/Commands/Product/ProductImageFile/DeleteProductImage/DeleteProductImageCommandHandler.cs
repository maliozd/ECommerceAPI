using ECommerceAPI.Application.Abstraction.Services.Product;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Product.ProductImageFile.DeleteProductImage
{
    public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommandRequest, DeleteProductImageCommandResponse>
    {
        readonly IProductService _productService;

        public DeleteProductImageCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<DeleteProductImageCommandResponse> Handle(DeleteProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _productService.DeleteProductImageFileAsync(request.Id, request.ImageId);
            return new()
            {
                Success = response
            };
        }
    }
}
