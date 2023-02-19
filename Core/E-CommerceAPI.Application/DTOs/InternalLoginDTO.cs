using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.DTOs
{
    public class InternalLoginDTO
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
