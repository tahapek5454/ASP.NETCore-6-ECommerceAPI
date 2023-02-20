using E_CommerceAPI.Application.DTOs;
using E_CommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Abstractions.Tokens
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(int second, AppUser user);
        string CreateRefreshToken();
    }
}
