using ECommerceAPI.Application.Repositories;
using MediatR;

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
            var data = await _productReadRepository.GetByIdAsync(Guid.Parse(request.Id));
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
