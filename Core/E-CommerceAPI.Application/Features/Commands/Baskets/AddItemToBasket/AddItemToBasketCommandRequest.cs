using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.Baskets.AddItemToBasket
{
    public class AddItemToBasketCommandRequest: IRequest<AddItemToBasketCommandResponse>
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
