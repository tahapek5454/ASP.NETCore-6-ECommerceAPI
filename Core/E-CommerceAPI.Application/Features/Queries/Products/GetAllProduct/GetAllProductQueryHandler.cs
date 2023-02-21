using E_CommerceAPI.Application.Repositories.ProductRepository;
using E_CommerceAPI.Application.RequestParameters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.Products.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        private IProductReadRepository _productReadRepository;

        public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }




        // CQRS patterınımızı yonetmek için mediatR kütüphanesinde ilgil handler classını yonetebilmek için bizden
        // request ve response sınıflarını ona tanıntmamızı istedi ve gerçekleşecek olayları ovveride ederek kodlamaya zorladı.
        // artık gelen istek nesnesine karşılık response nesnesi dönderecek
        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            int totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(request.Size * request.Page).Take(request.Size)
                .Include(p => p.ProductImageFiles)
                .Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreateDate,
                p.UpdateDate,
                p.ProductImageFiles
            }).OrderBy(p=> p.CreateDate).ToList();

            GetAllProductQueryResponse response = new GetAllProductQueryResponse()
            {
                TotalCount = totalCount,
                Products = products
            };

            return response;

        }
    }
}
