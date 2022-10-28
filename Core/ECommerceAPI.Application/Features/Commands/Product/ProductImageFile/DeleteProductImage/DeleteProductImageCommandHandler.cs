using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.ProductImageFileRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Product.ProductImageFile.DeleteProductImage
{
    public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommandRequest, DeleteProductImageCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductImageFileWriteRepository productImageFileWriteRepository;
        public DeleteProductImageCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            this.productImageFileWriteRepository = productImageFileWriteRepository;
        }
        public async Task<DeleteProductImageCommandResponse> Handle(DeleteProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == request.Id);
            Domain.Entities.FileEntities.ProductImageFile? productImageFile = product?.ProductImageFiles.FirstOrDefault(p => p.Id == request.ImageId);
            if (productImageFile != null)
            {
                product?.ProductImageFiles.Remove(productImageFile);
                productImageFileWriteRepository.Remove(productImageFile);
                await _productWriteRepository.SaveAsync();
                return new() { Success = true };
            }
            else
                return new() { Success = false };
           
        }
    }
}
