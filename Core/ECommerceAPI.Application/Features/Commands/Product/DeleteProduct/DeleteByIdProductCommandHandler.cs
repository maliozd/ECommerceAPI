using ECommerceAPI.Application.Abstraction.Services.Product;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Product.DeleteProduct
{
    public class DeleteByIdProductCommandHandler : IRequestHandler<DeleteByIdProductCommandRequest, DeleteByIdProductCommandResponse>
    {
        readonly IProductService _productService;
        public DeleteByIdProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<DeleteByIdProductCommandResponse> Handle(DeleteByIdProductCommandRequest request, CancellationToken cancellationToken)
        {
            var serviceResponse = await _productService.DeleteProductAsync(request.Id);
            return new()
            {
                Success = serviceResponse == true ? true : false,
            };
        }
    }
}
