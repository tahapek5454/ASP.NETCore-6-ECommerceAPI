using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.DTOs.Orders
{
    public class GetOrderDTO
    {
        public string Address { get; set; }
        public object BasketItems { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public string OrderCode { get; set; }
    }
}
