using E_CommerceAPI.Domain.Entities.Common;
using E_CommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Domain.Entities
{
    public class Basket: BaseEntity
    {
        public Order Order { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public ICollection<BasketItem> BasketItems { get;set; }

    }
}
