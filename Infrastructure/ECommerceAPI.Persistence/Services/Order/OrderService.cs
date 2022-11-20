using ECommerceAPI.Application.Abstraction.Repositories.CompletedOrderRepository;
using ECommerceAPI.Application.Abstraction.Services.Order;
using ECommerceAPI.Application.Dtos.Order;
using ECommerceAPI.Application.Features.Queries.Order.GetByIdOrder;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Persistence.Services.Basic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Services.Order
{
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly IOrderReadRepository _orderReadRepository;
        readonly ICompletedOrderReadRepository _completedOrderReadRepository;
        readonly ICompletedOrderWriteRepository _completedOrderWriteRepository;
        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ICompletedOrderWriteRepository completedOrderWriteRepository, ICompletedOrderReadRepository completedOrderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _completedOrderWriteRepository = completedOrderWriteRepository;
            _completedOrderReadRepository = completedOrderReadRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            await _orderWriteRepository.AddAsync(new()
            {
                Address = createOrderDto.Address,
                Id = Guid.Parse(createOrderDto.BasketId),
                Description = createOrderDto.Description,
                OrderCode = NameService.CreateOrderCode()
            });
            await _orderWriteRepository.SaveAsync();
        }

        public async Task<ListOrderDto> GetAllOrdersAsync(int page, int size)
        {
            var query = _orderReadRepository.Table.Include(o => o.Basket)
                      .ThenInclude(b => b.User)
                      .Include(o => o.Basket)
                         .ThenInclude(b => b.BasketItems)
                         .ThenInclude(bi => bi.Product);
            var data = query.Skip(page * size).Take(size);

            var data2 = from order in data
                        join completedOrder in _completedOrderReadRepository.Table
                           on order.Id equals completedOrder.OrderId into co
                        from _co in co.DefaultIfEmpty()
                        select new
                        {
                            Id = order.Id,
                            CreatedDate = order.CreatedDate,
                            OrderCode = order.OrderCode,
                            Basket = order.Basket,
                            Completed = _co == null ? false : true
                        };
            return new()
            {
                TotalOrderCount = await query.CountAsync(),
                Orders = await data2.Select(o => new
                {
                    Id = o.Id.ToString(),
                    CreatedDate = o.CreatedDate,
                    OrderCode = o.OrderCode,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                    UserName = o.Basket.User.UserName,
                    isCompleted = o.Completed
                }).ToListAsync()
            };

        }

        public async Task<bool> DeleteOrderAsync(string id)
        {
            await _orderWriteRepository.Remove(id);           
            var result = await _orderWriteRepository.SaveAsync();
            return result > 1 ? true : false;
        }

        public async Task<SingleOrder> GetOrderByIdAsync(string id)
        {
            var query =  _orderReadRepository.Table.
                Include(o => o.Basket).
                ThenInclude(b => b.BasketItems).
                ThenInclude(bi => bi.Product);

            var queryObject = await (from order in query     //anonymous object created from query
                         join completedOrder in _completedOrderReadRepository.Table
                          on order.Id equals completedOrder.OrderId into com
                         from _com in com.DefaultIfEmpty()
                         select new
                         {
                             Id = order.Id,
                             CreatedDate = order.CreatedDate,
                             OrderCode = order.OrderCode,
                             Basket = order.Basket,
                             isCompleted = _com == null ? false : true,
                             Address = order.Address,
                             Description = order.Description
                         }).FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));
                                     
            return new()
            {
                Id = queryObject.Id.ToString(),
                BasketItems = queryObject.Basket.BasketItems.Select(bi => new
                {
                    bi.Product.Name,
                    bi.Product.Price,
                    bi.Quantity
                }),
                Address = queryObject.Address,
                CreatedDate = queryObject.CreatedDate,
                OrderCode = queryObject.OrderCode,
                Description = queryObject.Description,
                isCompleted = queryObject.isCompleted
            };
        }

        public async Task<bool> CompleteOrderAsync(string id)
        {
            ECommerceAPI.Domain.Entities.Order order = await _orderReadRepository.GetByIdAsync(id);
            if (order == null)
                throw new Exception("Order not found");

            await _completedOrderWriteRepository.AddAsync(new()
            {
                OrderId = Guid.Parse(id)
            });
            var result = await _completedOrderWriteRepository.SaveAsync();
            return result == 1 ? true : false;
        }
    }
}
