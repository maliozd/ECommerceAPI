using ECommerceAPI.Application.Repositories;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductReadRepository _productReadRepository;
        public UpdateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;

        }
        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var productToUpdate = await _productReadRepository.GetByIdAsync(Guid.Parse(request.Id));
            productToUpdate.Stock = request.Stock;
            productToUpdate.Name = request.Name;
            productToUpdate.Price = request.Price;
            //var response = _productWriteRepository.Update(productToUpdate);
            await _productWriteRepository.SaveAsync();
            return new();
            //{
            //    Success = response ? true : false
            //}
        }
    }
}
