using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.ProductImageFiles.GetProductImage
{
    public class GetProductImagegQueryRequest: IRequest<List<GetProductImageQueryResponse>>
    {
        public string Id { get; set; }   
    }
}
