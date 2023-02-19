using E_CommerceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Abstractions.Services.Authentications
{
    public interface IInternalAuthenticationService
    {
        Task<Token> LoginAsync(InternalLoginDTO model, int accessTokenLifeTime);
    }
}
