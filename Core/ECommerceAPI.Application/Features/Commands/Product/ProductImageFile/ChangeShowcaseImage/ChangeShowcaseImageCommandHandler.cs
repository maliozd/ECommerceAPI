using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.ProductImageFileRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Product.ProductImageFile.ChangeShowcaseImage
{
    public class ChangeShowcaseImageCommandHandler : IRequestHandler<ChangeShowcaseImageCommandRequest, ChangeShowcaseImageCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        IProductImageFileReadRepository _productImageFileReadRepository;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public ChangeShowcaseImageCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository, IProductReadRepository productReadRepository, IProductImageFileReadRepository productImageFileReadRepository)
        {
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _productReadRepository = productReadRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
        }

        public async Task<ChangeShowcaseImageCommandResponse> Handle(ChangeShowcaseImageCommandRequest request, CancellationToken cancellationToken)
        {
            //var imageFile = await _productImageFileReadRepository.Table.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == request.ImageId);
            //var product = imageFile.Products.FirstOrDefault(x => x.Id == request.ProductId);
            var product = await _productReadRepository.Table.Where(p => p.Id == request.ProductId).Include(x => x.ProductImageFiles).FirstOrDefaultAsync();
            var images = product.ProductImageFiles;
            foreach (var image in images)
            {
                if (image.Showcase == true)
                    image.Showcase = false;
                if (image.Id == request.ImageId)
                    image.Showcase = true;
            }
            await _productImageFileWriteRepository.SaveAsync();
            return new();

        }
    }
}
