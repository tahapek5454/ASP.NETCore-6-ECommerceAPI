using E_CommerceAPI.Application.Abstractions.Services.Authentications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Abstractions.Services
{
    public interface IAuthService: IExternalAuthenticationService, IInternalAuthenticationService
    {
        Task PasswordResetAsync(string email);
        Task<bool> VerifyResetTokenAsync(string userId, string resetToken);
    }
}
