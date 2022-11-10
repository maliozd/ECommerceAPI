using ECommerceAPI.Application.Abstraction.Services.Product;
using ECommerceAPI.Application.Abstraction.Storage;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.ProductImageFileRepository;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Product.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        readonly IProductService _productService;

        public UploadProductImageCommandHandler(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _productService.UploadProductImageFileAsync(request.Id, request.Files);

            return new() { Success = response };

        }
    }
}
