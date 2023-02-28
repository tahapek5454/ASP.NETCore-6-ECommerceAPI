using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.DTOs.Orders;
using E_CommerceAPI.Application.Repositories.OrderRepository;
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


        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
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

            var query = await _orderReadRepository.Table.Include(o => o.Basket)
                .ThenInclude(b => b.User)
                .Include(o => o.Basket)
                .ThenInclude(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .Select(o => new
                {
                    Id = o.Id.ToString(),
                    CreatedDate = o.CreateDate,
                    OrderCode = o.OrderCode,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                    UserName = o.Basket.User.UserName
                }).Skip(page * size).Take(size).ToListAsync();
                

            return new()
            {
                TotalOrderCount = totalCount,
                Orders = query

            };

        }

        public async Task<GetOrderDTO> GetOrderByIdAsync(string id)
        {
            var data = await _orderReadRepository.Table
                    .Include(o => o.Basket)
                    .ThenInclude(b => b.BasketItems)
                    .ThenInclude(bi => bi.Product)
                    .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

            return new GetOrderDTO()
            {
                Id = data.Id.ToString(),
                Address = data.Address,
                BasketItems = data.Basket.BasketItems.Select(bi => new
                {
                    bi.Product.Name,
                    bi.Product.Price,
                    bi.Quantity
                }),
                CreatedDate = data.CreateDate,
                Description = data.Description,
                OrderCode = data.OrderCode,
            };
           
        }
    }
}
