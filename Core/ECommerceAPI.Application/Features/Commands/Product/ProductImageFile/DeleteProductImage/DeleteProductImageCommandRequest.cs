using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Product.ProductImageFile.DeleteProductImage
{
    public class DeleteProductImageCommandRequest : IRequest<DeleteProductImageCommandResponse>
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
    }
}
