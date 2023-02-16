using E_CommerceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.AppUsers.LoginUser
{
    public class LoginUserCommandResponse
    {
    }

    public class LoginUserSuccessCommandResponse: LoginUserCommandResponse
    {
        public Token Token { get; set; } 
    }

    public class LoginUserErrorCommandResponse: LoginUserCommandResponse
    {
        public string Message { get; set; }
    }
}
