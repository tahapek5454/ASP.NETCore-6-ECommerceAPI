using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.DTOs.Users
{
    public class ListUserDTO
    {
        public string Id { get; set; }
        public string  Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string UserName { get; set; }
    }
}
