using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.DTOs.Orders;
using E_CommerceAPI.Application.Exceptions;
using E_CommerceAPI.Application.Repositories.CompletedOrderRepository;
using E_CommerceAPI.Application.Repositories.OrderRepository;
using E_CommerceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly ICompletedOrderWriteRepository _completedOrderWriteRepository;
        private readonly ICompletedOrderReadRepository _completedOrderReadRepository;


        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ICompletedOrderWriteRepository completedOrderWriteRepository, ICompletedOrderReadRepository completedOrderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _completedOrderWriteRepository = completedOrderWriteRepository;
            _completedOrderReadRepository = completedOrderReadRepository;
        }

        public async Task CompleteOrderAsync(string id)
        {
            Order order = await _orderReadRepository.GetByIdAsync(id);
            if(order != null)
            {
                await _completedOrderWriteRepository.AddAsync(new CompletedOrder()
                {
                    OrderId = Guid.Parse(id)
                });

                _ = await _completedOrderWriteRepository.SaveAsync();
            }
            else
            {
                throw new OrderNotFoundException(OrderNotFoundException.Message);
            }
        }

        public async Task CreateOrderAsync(CreateOrderDTO createOrderDTO)
        {
            var orderCode = (new Random().NextDouble() * 10000).ToString();
            orderCode = orderCode.Substring(orderCode.IndexOf(",")+1, orderCode.Length- orderCode.IndexOf(",")-1);
            await _orderWriteRepository.AddAsync(new()
            {
                Address = createOrderDTO.Address,
                Id = Guid.Parse(createOrderDTO.BasketId),
                Description = createOrderDTO.Description,
                OrderCode = orderCode
            });

            await _orderWriteRepository.SaveAsync();
        }

        public async Task<ListOrderDTO> GetAllOrdersAsync(int page, int size)
        {
            int totalCount = _orderReadRepository.GetAll(false).Count();

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
                         order.Id,
                         order.CreateDate,
                         order.OrderCode,
                         order.Basket,
                         Completed = _co != null ? true : false
                     };


            return new()
            {
                TotalOrderCount = totalCount,
                Orders = data2.Select(o => new
                {
                    Id = o.Id.ToString(),
                    CreatedDate = o.CreateDate,
                    OrderCode = o.OrderCode,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                    UserName = o.Basket.User.UserName,
                    Completed = o.Completed,  
                }).ToList(),

            };

        }

        public async Task<GetOrderDTO> GetOrderByIdAsync(string id)
        {
            var data =  _orderReadRepository.Table
                    .Include(o => o.Basket)
                    .ThenInclude(b => b.BasketItems)
                    .ThenInclude(bi => bi.Product);

            var data2 = await (from order in data
                        join completedOrder in _completedOrderReadRepository.Table
                        on order.Id equals completedOrder.OrderId into co
                        from _co in co.DefaultIfEmpty()
                        select new
                        {

                            order.Id,
                            order.CreateDate,
                            order.OrderCode,
                            order.Basket,
                            Completed = _co != null ? true : false,
                            Address = order.Address,
                            Description = order.Description

                        }).FirstOrDefaultAsync(o => o.Id == Guid.Parse(id)); 
                   

            return new GetOrderDTO()
            {
                Id = data2.Id.ToString(),
                Address = data2.Address,
                BasketItems = data2.Basket.BasketItems.Select(bi => new
                {
                    bi.Product.Name,
                    bi.Product.Price,
                    bi.Quantity
                }),
                CreatedDate = data2.CreateDate,
                Description = data2.Description,
                OrderCode = data2.OrderCode,
                Completed = data2.Completed,
            };
           
        }
    }
}
