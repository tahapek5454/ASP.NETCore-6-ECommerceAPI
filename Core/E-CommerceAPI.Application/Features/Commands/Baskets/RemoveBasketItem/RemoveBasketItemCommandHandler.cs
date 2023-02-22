using E_CommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.Baskets.RemoveBasketItem
{
    public class RemoveBasketItemCommandHandler : IRequestHandler<RemoveBasketItemCommandRequest, RemoveBasketItemCommandResponse>
    {
        private readonly IBasketService _basketService;

        public RemoveBasketItemCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<RemoveBasketItemCommandResponse> Handle(RemoveBasketItemCommandRequest request, CancellationToken cancellationToken)
        {
            await _basketService.RemoveBasketItemAsync(request.BasketItemId);

            return new();
        }
    }
}
