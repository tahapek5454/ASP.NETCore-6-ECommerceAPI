using E_CommerceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.AppUsers.GoogleLogin
{
    public class GoogleLoginCommandResponse
    {
        public Token Token { get; set; }
    }

    // GoogleAPI keyimizi gizli almak için dosyadan okuma yapılmaktadır o amaçla kuruldu
    public class APIKey
    {
        public APIValue web { get; set; }
    }
    public class APIValue
    {
        public string client_id { get; set; }
    }
}
