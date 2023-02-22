using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.Baskets.GetBasketItems
{
    public class GetBasketItemQueryResponse
    {
        public string BasketItemId { get; set; }  
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
