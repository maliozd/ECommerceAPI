using ECommerceAPI.Application.ViewModels.Baskets;
using ECommerceAPI.Domain.Entities.BasketEntities;

namespace ECommerceAPI.Application.Abstraction.Services.Basket
{
    public interface IBasketService
    {
        Task<List<BasketItem>> GetBasketItemsAsync();
        Task AddItemAsync(VM_Create_BasketItem basketItem);
        Task UpdateQuantityAsync(VM_Update_BasketItem basketItem);
        Task RemoveItemAsync(int basketItemId);
    }
}
