using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.Baskets.UpdateQuantity
{
    public class UpdateQuantityCommandRequest: IRequest<UpdateQuantityCommandResponse>
    {
        public string BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}
