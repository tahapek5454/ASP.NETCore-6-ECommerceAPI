using E_CommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.Baskets.UpdateQuantity
{
    public class UpdateQuantityCommandHandler : IRequestHandler<UpdateQuantityCommandRequest, UpdateQuantityCommandResponse>
    {
        private readonly IBasketService _basketService;

        public UpdateQuantityCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<UpdateQuantityCommandResponse> Handle(UpdateQuantityCommandRequest request, CancellationToken cancellationToken)
        {
            await _basketService.UpdateQuantityAsync(new()
            {
                BasketItemId = request.BasketItemId,
                Quantity = request.Quantity,
            });

            return new();
        }
    }
}
