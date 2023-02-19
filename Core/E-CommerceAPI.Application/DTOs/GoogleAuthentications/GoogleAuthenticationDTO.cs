using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.DTOs.GoogleAuthentications
{
    public class GoogleAuthenticationDTO
    {
        public string IdToken { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
