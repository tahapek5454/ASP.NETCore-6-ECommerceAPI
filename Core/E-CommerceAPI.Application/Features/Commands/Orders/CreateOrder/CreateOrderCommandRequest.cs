using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.Orders.CreateOrder
{
    public class CreateOrderCommandRequest: IRequest<CreateOrderCommandResponse>
    {
        public string Description { get; set; }
        public string Address { get; set; }
    }
}
