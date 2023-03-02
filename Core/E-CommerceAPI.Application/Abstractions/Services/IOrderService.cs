using E_CommerceAPI.Application.DTOs.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderDTO createOrderDTO);

        Task<ListOrderDTO> GetAllOrdersAsync(int page, int size);
        Task<GetOrderDTO> GetOrderByIdAsync(string id);
        Task CompleteOrderAsync(string id);
    }
}
