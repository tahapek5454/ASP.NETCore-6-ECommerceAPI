using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.DTOs.Orders;
using E_CommerceAPI.Application.Repositories.OrderRepository;
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

        public OrderService(IOrderWriteRepository orderWriteRepository)
        {
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDTO createOrderDTO)
        {
            await _orderWriteRepository.AddAsync(new()
            {
                Address = createOrderDTO.Address,
                Id = Guid.Parse(createOrderDTO.BasketId),
                Description = createOrderDTO.Description,
            });

            await _orderWriteRepository.SaveAsync();
        }
    }
}
