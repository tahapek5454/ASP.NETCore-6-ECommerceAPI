using E_CommerceAPI.Application.Features.Commands.Baskets.AddItemToBasket;
using E_CommerceAPI.Application.Features.Commands.Baskets.RemoveBasketItem;
using E_CommerceAPI.Application.Features.Commands.Baskets.UpdateQuantity;
using E_CommerceAPI.Application.Features.Queries.Baskets.GetBasketItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")] // bunun sayesinde kullanici adini aliyoruz
    public class BasketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasketItems([FromQuery]GetBasketItemQueryRequest getBasketItemQueryRequest)
        {
            // from query yapma seebimiz bos gondercez ya ody de deger bekleyip hataya sebep olmasin
            List<GetBasketItemQueryResponse> reponse = await _mediator.Send(getBasketItemQueryRequest);

            return Ok(reponse);

        }

        [HttpPost]
        public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommandRequest addItemToBasketCommandRequest)
        {
            AddItemToBasketCommandResponse response = await _mediator.Send(addItemToBasketCommandRequest);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommandRequest updateQuantityCommandRequest)
        {
            UpdateQuantityCommandResponse response = await _mediator.Send(updateQuantityCommandRequest);

            return Ok(response);
        }

        [HttpDelete("{BasketItemId}")]
        public async Task<IActionResult> RemoveBasketItem([FromRoute]RemoveBasketItemCommandRequest removeBasketItemCommandRequest)
        {
            RemoveBasketItemCommandResponse response = await _mediator.Send(removeBasketItemCommandRequest);

            return Ok(response);

        }
    }
}
