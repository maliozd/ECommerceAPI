using ECommerceAPI.Application.Dtos.BasketItems;
using ECommerceAPI.Domain.Entities.BasketEntities;

namespace ECommerceAPI.Application.Abstraction.Services.Basket
{
    public interface IBasketService
    {
        Task<List<BasketItem>> GetBasketItemsAsync();
        Task AddItemAsync(CreateBasketItemDto basketItem);
        Task UpdateQuantityAsync(UpdateBasketItemDto basketItem);
        Task RemoveItemAsync(string basketItemId);
        ECommerceAPI.Domain.Entities.BasketEntities.Basket? UserActiveBasket { get; }
    }
}
