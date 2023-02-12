using E_CommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Domain.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>(); // birden fazla orderda bulunabilir
        public ICollection<ProductImageFile> ProductImageFiles { get; set; } // birden fazla foto bulunabilir
    }
}
