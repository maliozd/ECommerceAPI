using ECommerceAPI.Application.Abstraction.Hubs;
using ECommerceAPI.Application.Repositories;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        IProductWriteRepository _productWriteRepository;
        IProductHubService _productHubService;
        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductHubService productHubService = null)
        {
            _productWriteRepository = productWriteRepository;
            _productHubService = productHubService;
        }
        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                Stock = request.Stock,
                Price = request.Price
            });
            var response = await _productWriteRepository.SaveAsync();
            if (response > 0)
            {
                await _productHubService.ProductAddedMessageAsync($"{request.Name} successfully added. --Sent by SignalR--");
                return new()
                {
                    Success = true
                };
            }
            return new() { Success = false };
        }
    }
}
