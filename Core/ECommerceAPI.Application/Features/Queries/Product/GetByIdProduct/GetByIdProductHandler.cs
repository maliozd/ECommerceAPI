using ECommerceAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductHandler : IRequestHandler<GetByIdProductRequest, GetByIdProductResponse>
    {
        readonly IProductReadRepository _productReadRepository;

        public GetByIdProductHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }
        public async Task<GetByIdProductResponse> Handle(GetByIdProductRequest request, CancellationToken cancellationToken)
        {
            var data = await _productReadRepository.GetByIdAsync(request.Id);
            if (data == null)
                throw new NullReferenceException("Invalid id");
            return new()
            {
                Name = data.Name,
                Stock = data.Stock,
                Price = data.Price
            };
        }
    }
}
