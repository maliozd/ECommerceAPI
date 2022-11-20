using ECommerceAPI.Application.Dtos.Order;

namespace ECommerceAPI.Application.Abstraction.Services.Order
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<ListOrderDto> GetAllOrdersAsync(int page, int size);
        Task<SingleOrder> GetOrderByIdAsync(string id);
        Task<bool> DeleteOrderAsync(string id);
        Task<bool> CompleteOrderAsync(string id);
    }
}
