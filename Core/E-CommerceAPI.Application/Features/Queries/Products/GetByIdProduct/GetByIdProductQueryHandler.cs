using E_CommerceAPI.Application.Repositories.ProductRepository;
using E_CommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.Products.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        public GetByIdProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
           Product product = await _productReadRepository.GetByIdAsync(request.Id, false);
           GetByIdProductQueryResponse response = new GetByIdProductQueryResponse() {
            
               Name = product.Name,
               Price= product.Price,
               Stock= product.Stock,
           
           };

            return response;
        }
    }
}
