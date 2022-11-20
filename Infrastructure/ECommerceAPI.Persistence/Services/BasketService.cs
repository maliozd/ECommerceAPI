using ECommerceAPI.Application.Abstraction.Repositories.BasketItemRepository;
using ECommerceAPI.Application.Abstraction.Repositories.BasketRepository;
using ECommerceAPI.Application.Abstraction.Services.Basket;
using ECommerceAPI.Application.Dtos.BasketItems;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.BasketEntities;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Services
{
    public class BasketService : IBasketService
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly UserManager<AppUser> _userManager;
        readonly IOrderReadRepository _orderReadRepository;
        readonly IBasketWriteRepository _basketWriteRepository;
        readonly IBasketItemWriteRepository _basketItemWriteRepository;
        readonly IBasketItemReadRepository _basketItemReadRepository;
        readonly IBasketReadRepository _basketReadRepository;

        public Basket? UserActiveBasket
        {
            get
            {
                Basket basket = GetUserBasketAsync().Result;
                return basket;
            }
        }

        public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, IBasketWriteRepository basketWriteRepository, IBasketItemWriteRepository basketItemWriteRepository, IBasketItemReadRepository basketItemReadRepository, IBasketReadRepository basketReadRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
            _basketItemReadRepository = basketItemReadRepository;
            _basketReadRepository = basketReadRepository;
        }

        private async Task<Basket> GetUserBasketAsync()
        {
            var username = _httpContextAccessor.HttpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(username))
            {
                AppUser? appUser = await _userManager.Users.Include(u => u.Baskets).FirstOrDefaultAsync(u => u.UserName == username);
                
                var _basket = from basket in appUser.Baskets
                              join order in _orderReadRepository.Table
                              on basket.Id equals order.Id into basketOrder
                              from order in basketOrder.DefaultIfEmpty()
                              select new
                              {
                                  basket = basket,
                                  Order = order,
                              };
                Basket? targetBasket = null;
                if (_basket.Any(b => b.Order is null))  //active basket
                {
                    targetBasket = _basket.FirstOrDefault(b => b.Order is null)?.basket;
                }
                else
                {
                    targetBasket = new();
                    appUser.Baskets.Add(targetBasket);
                }

                await _basketWriteRepository.SaveAsync();
                return targetBasket;
            }
            throw new Exception("User not found");
        }

        public async Task AddItemAsync(CreateBasketItemDto basketItem)
        {
            Basket basket = await GetUserBasketAsync();
            if (basket != null)
            {
                BasketItem _basketItem = await _basketItemReadRepository.GetSingleAsync(bi => bi.BasketId == basket.Id && bi.ProductId == Guid.Parse(basketItem.ProductId));
                if (_basketItem != null)
                    _basketItem.Quantity++;
                else
                    await _basketItemWriteRepository.AddAsync(new()
                    {
                        BasketId = basket.Id,
                        ProductId = Guid.Parse(basketItem.ProductId),
                        Quantity = basketItem.Quantity
                    });
                await _basketItemWriteRepository.SaveAsync();
            }
        }

        public async Task<List<BasketItem>> GetBasketItemsAsync()
        {
            Basket basket = await GetUserBasketAsync();
            Basket? result = await _basketReadRepository.Table.
                Include(b => b.BasketItems).
                ThenInclude(bi => bi.Product).
                FirstOrDefaultAsync(b => b.Id == basket.Id);

            return result.BasketItems.ToList();
        }

        public async Task RemoveItemAsync(string basketItemId)
        {
            BasketItem basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);
            if (basketItem != null)
            {
                _basketItemWriteRepository.Remove(basketItem);
                await _basketItemWriteRepository.SaveAsync();
            }
        }

        public async Task UpdateQuantityAsync(UpdateBasketItemDto basketItem)
        {
            var targetBasketItem = await _basketItemReadRepository.GetByIdAsync(basketItem.BasketItemId);
            if (targetBasketItem != null)
            {
                targetBasketItem.Quantity = basketItem.Quantity;
                await _basketItemWriteRepository.SaveAsync();
            }
        }


    }
}
