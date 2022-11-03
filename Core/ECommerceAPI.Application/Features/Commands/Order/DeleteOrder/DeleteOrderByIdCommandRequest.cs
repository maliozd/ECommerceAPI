using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Order.DeleteOrder
{
    public class DeleteOrderByIdCommandRequest : IRequest<DeleteOrderByIdCommandResponse>
    {
        public int Id { get; set; }
    }
}