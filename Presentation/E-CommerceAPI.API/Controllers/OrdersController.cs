using E_CommerceAPI.Application.Features.Commands.Orders.CreateOrder;
using E_CommerceAPI.Application.Features.Queries.Orders.GetAllOrders;
using E_CommerceAPI.Application.Features.Queries.Orders.GetOrderById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest)
        {
            CreateOrderCommandResponse reponse = await _mediator.Send(createOrderCommandRequest);

            return Ok(reponse);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery]GetAllOrderQueryRequest getAllOrderQueryRequest)
        {
            GetAllOrderQueryResponse response = await _mediator.Send(getAllOrderQueryRequest);
            

            return Ok(response);

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOrderById([FromRoute]GetOrderByIdQueryRequest getOrderByIdQueryRequest)
        {
            GetOrderByIdQueryResponse response =await _mediator.Send(getOrderByIdQueryRequest);

            return Ok(response);
        }


    }
}
