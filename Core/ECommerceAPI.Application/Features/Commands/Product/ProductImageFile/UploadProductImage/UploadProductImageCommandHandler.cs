using ECommerceAPI.Application.Abstraction.Storage;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.ProductImageFileRepository;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Product.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly IStorageService _storageService;

        public UploadProductImageCommandHandler(IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IStorageService storageService)
        {
            _productReadRepository = productReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _storageService = storageService;
        }
        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {

            List<(string fileName, string pathOrContainer)> result = await _storageService.UploadAsync("photo-images", request.Files);

            var product = await _productReadRepository.GetByIdAsync(request.Id);
            await _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new Domain.Entities.FileEntities.ProductImageFile
            {
                FileName = r.fileName,
                Path = r.pathOrContainer,
                Storage = _storageService.StorageName,
                Products = new List<Domain.Entities.Product>() { product },

            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();
            return new();
        }
    }
}
