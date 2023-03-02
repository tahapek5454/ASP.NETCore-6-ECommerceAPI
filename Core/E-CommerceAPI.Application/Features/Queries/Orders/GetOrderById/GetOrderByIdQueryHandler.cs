using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.DTOs.Orders;
using E_CommerceAPI.Application.Repositories.OrderRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.Orders.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryRequest, GetOrderByIdQueryResponse>
    {
        private readonly IOrderService _orderService;

        public GetOrderByIdQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
        {
            GetOrderDTO orderDTO = await _orderService.GetOrderByIdAsync(request.Id);

            return new()
            {
                Id = orderDTO.Id,
                Address = orderDTO.Address,
                BasketItems = orderDTO.BasketItems,
                CreatedDate = orderDTO.CreatedDate,
                Description = orderDTO.Description,
                OrderCode = orderDTO.OrderCode,
                Completed = orderDTO.Completed,
            };
        }
    }
}
