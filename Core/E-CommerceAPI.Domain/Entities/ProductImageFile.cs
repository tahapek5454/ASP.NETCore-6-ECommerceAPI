using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Domain.Entities
{
    public class ProductImageFile:OwnFile
    {
        public ICollection<Product> Products { get; set; } // bir ressim birden fazla urune ait olabilir
    }
}
