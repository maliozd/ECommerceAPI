using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Basket.RemoveBasketItem
{
    public class RemoveBasketItemCommandRequest : IRequest<RemoveBasketItemCommandResponse>
    {
        public int BasketItemId { get; set; }
    }
}