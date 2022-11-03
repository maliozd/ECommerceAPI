using ECommerceAPI.Application.Dtos.Order;

namespace ECommerceAPI.Application.Abstraction.Services.Order
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<ListOrderDto> GetAllOrdersAsync(int page, int size);
        Task<SingleOrder> GetOrderByIdAsync(int id);
        Task<bool> DeleteOrderAsync(int id);
    }
}
