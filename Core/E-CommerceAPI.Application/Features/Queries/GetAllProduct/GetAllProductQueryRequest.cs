using E_CommerceAPI.Application.RequestParameters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.GetAllProduct
{
    public class GetAllProductQueryRequest:IRequest<GetAllProductQueryResponse>
    {
        //mediatR kütüphanesine CQRS patternımıza yardımcı olması için başvuruyoruz
        //Bu class gelen istek parametrelerini tutacaktır
        //Biz mediatR dan gelen IRequest interfacini kullanarak kütüphanemize bu sınıfın request sınıfı oldugunu
        // ve donus responsundan GetAllProductQUERYrESPONSE oldugunu soyledik kutuphane kendisi yonetecek

        //public Pagination Pagination { get; set; }
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
