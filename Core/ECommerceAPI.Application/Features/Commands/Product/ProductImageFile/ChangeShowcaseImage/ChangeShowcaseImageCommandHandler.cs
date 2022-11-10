using ECommerceAPI.Application.Abstraction.Services.Product;
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
        readonly IProductService _productService;

        public ChangeShowcaseImageCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ChangeShowcaseImageCommandResponse> Handle(ChangeShowcaseImageCommandRequest request, CancellationToken cancellationToken)
        {

            var response = await _productService.ChangeProductImageShowcaseImageAsync(request.ProductId, request.ImageId);
            return new()
            {
                Success = response
            };
        }
    }
}
