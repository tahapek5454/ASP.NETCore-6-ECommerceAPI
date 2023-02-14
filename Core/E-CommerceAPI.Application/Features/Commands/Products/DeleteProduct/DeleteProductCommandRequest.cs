using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.Products.DeleteProduct
{
    public class DeleteProductCommandRequest: IRequest<DeleteProductCommandResponse>
    {
        public string Id { get; set; }
    }
}
