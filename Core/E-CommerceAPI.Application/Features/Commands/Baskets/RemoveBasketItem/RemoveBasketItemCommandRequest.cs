using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.Baskets.RemoveBasketItem
{
    public class RemoveBasketItemCommandRequest: IRequest<RemoveBasketItemCommandResponse>
    {
        public string BasketItemId { get; set; }
    }
}
