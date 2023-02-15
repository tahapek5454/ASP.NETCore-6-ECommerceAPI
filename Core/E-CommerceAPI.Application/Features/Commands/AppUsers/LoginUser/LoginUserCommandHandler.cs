using E_CommerceAPI.Application.Exceptions;
using E_CommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.AppUsers.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager; // -> bu da ıdentityden gelir sign işlemleri için hizmet veren servis

        public LoginUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            // kullanciyi ariyoruz once useerNmae ya da emaile gore
            AppUser user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
            if(user == null)
            {
                user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
            }

            if (user == null)
                throw new UserNotFoundException(UserNotFoundException.Message);

            //boolen sonuç döndürür authantica oldun mu oldmadın mı appUserda var mı bu user sifre ona bakar kendi hasler falan o islemleri kendi yapıyor cozuyor
            SignInResult signInResult =  await _signInManager.CheckPasswordSignInAsync(user, request.Password, false); // false hatalı durumda kitleme mekanizması

            if(signInResult.Succeeded) // Authentication basarili
            {
                // yetkiler belirlenecek
            }

            return new();
        }
    }
}
