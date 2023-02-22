using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.Baskets.GetBasketItems
{
    public class GetBasketItemQueryRequest: IRequest<List<GetBasketItemQueryResponse>>
    {
    }
}
