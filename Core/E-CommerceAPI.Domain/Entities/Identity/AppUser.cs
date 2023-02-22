using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Domain.Entities.Identity
{
    public class AppUser: IdentityUser<string>
    {
        // IdentityUser ilgil prooertiyleri barındırıyor ekstramız varsa bu sekil ekliyoruz
        // Identity turunu string olarak belirledik o arkada guid uretecektir -> Tabloları db ye bildirmelisin
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<Basket> Baskets { get; set; }

        //refresh Token le ilgili propertyler
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
    }
}
