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
    public class GetByIdProductImageFileQueryHandler : IRequestHandler<GetByIdProductImageFileQueryRequest, GetByIdProductImageFileQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IConfiguration _configuration;


        public GetByIdProductImageFileQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
        {
            _productReadRepository = productReadRepository;
            _configuration = configuration;
        }

        public async Task<GetByIdProductImageFileQueryResponse> Handle(GetByIdProductImageFileQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == request.Id);
            var image = product?.ProductImageFiles.Select(p => new
            {
                Path = $"{_configuration["BaseStorageUrl"]}/{p.Path}",
                p.FileName,
                p.Id
            });
            return new();
        }
    }
}

