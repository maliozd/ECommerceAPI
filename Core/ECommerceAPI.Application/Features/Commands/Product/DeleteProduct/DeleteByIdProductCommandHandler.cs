using ECommerceAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Product.DeleteProduct
{
    public class DeleteByIdProductCommandHandler : IRequestHandler<DeleteByIdProductCommandRequest, DeleteByIdProductCommandResponse>
    {
        IProductWriteRepository _productWriteRepository;
        public DeleteByIdProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }
        public async Task<DeleteByIdProductCommandResponse> Handle(DeleteByIdProductCommandRequest request, CancellationToken cancellationToken)
        {
            var repoResponse = await _productWriteRepository.Remove(request.Id);
            var saveResponse = await _productWriteRepository.SaveAsync();
            if (repoResponse == true && saveResponse > 0)
                return new() { Success = true };

            else
                return new() { Success = false };
        }
    }
}
