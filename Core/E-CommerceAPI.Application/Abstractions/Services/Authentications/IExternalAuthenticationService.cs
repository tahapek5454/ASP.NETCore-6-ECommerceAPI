using E_CommerceAPI.Application.DTOs;
using E_CommerceAPI.Application.DTOs.GoogleAuthentications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Abstractions.Services.Authentications
{
    public interface IExternalAuthenticationService
    {
        Task<Token> GoogleLoginAsync(GoogleAuthenticationDTO model, int accessTokenLifeTime);
    }
}
