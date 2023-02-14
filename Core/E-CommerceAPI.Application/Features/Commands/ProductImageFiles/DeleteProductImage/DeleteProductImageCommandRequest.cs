using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.ProductImageFiles.DeleteProductImage
{
    public class DeleteProductImageCommandRequest: IRequest<DeleteProductImageCommandResponse>
    {
        public string ProductId { get; set; }
        public string? ImageId { get; set; }
    }
}
