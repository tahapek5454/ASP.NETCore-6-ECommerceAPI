using E_CommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.Baskets.AddItemToBasket
{
    public class AddItemToBasketCommandHandler : IRequestHandler<AddItemToBasketCommandRequest, AddItemToBasketCommandResponse>
    {
        private readonly IBasketService _basketService;

        public AddItemToBasketCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<AddItemToBasketCommandResponse> Handle(AddItemToBasketCommandRequest request, CancellationToken cancellationToken)
        {
            await _basketService.AddItemToBasketAsync(new ViewModels.Baskets.VM_Create_BasketItem()
            {
                ProductId= request.ProductId,
                Quantity = request.Quantity,
            });

            return new AddItemToBasketCommandResponse();
        }
    }
}
