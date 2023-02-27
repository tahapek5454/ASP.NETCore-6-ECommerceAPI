using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.DTOs.Orders
{
    public class ListOrderDTO
    {
        public int TotalOrderCount { get; set; }
        public object Orders { get; set; }
    }
}
