using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Basket.UpdateQuantity
{
    public class UpdateQuantityCommandRequest : IRequest<UpdateQuantityCommandResponse>
    {
        public int BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}