using ECommerceAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.ProductImageFile.GetByIdProductImageFile
{
    public class GetByIdProductImageFileQueryHandler : IRequestHandler<GetByIdProductImageFileQueryRequest, List<GetByIdProductImageFileQueryResponse>>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IConfiguration configuration;


        public GetByIdProductImageFileQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
        {
            _productReadRepository = productReadRepository;
            this.configuration = configuration;
        }

        public async Task<List<GetByIdProductImageFileQueryResponse>> Handle(GetByIdProductImageFileQueryRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                              .FirstOrDefaultAsync(p => p.Id == request.Id);

            if (product?.ProductImageFiles.Count > 0)
            {
                return product.ProductImageFiles.Select(p => new GetByIdProductImageFileQueryResponse
                {
                    Id = p.Id,
                    Path = $"{configuration["BaseStorageUrl"]}/{p.Path}",
                    FileName = p.FileName,
                    Showcase = p.Showcase,
                    
                }).ToList();
            }
            return new();
        }

    }
}

