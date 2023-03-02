using E_CommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Domain.Entities
{
    public class Order:BaseEntity
    {
        public string Description { get; set; }
        public string Address { get; set; }
        //public ICollection<Product> Products { get; set; } = new List<Product>(); // birden fazla producta sahip olabilirsin
        //public int CustomerId { get; set; }  // bunu vermesek de alttaki yapılan tanımlamadan dolayı vertabanında stun olusurdu
        // public Customer Customer { get; set; } // tek bir customer'ın olabilir

        public string OrderCode { get; set; }
        public Basket Basket { get; set; }
        public CompletedOrder CompletedOrder { get; set; }
    }
}
