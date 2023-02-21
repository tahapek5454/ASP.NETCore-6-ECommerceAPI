using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.Abstractions.Tokens;
using E_CommerceAPI.Application.DTOs;
using E_CommerceAPI.Application.DTOs.GoogleAuthentications;
using E_CommerceAPI.Application.Exceptions;
using E_CommerceAPI.Application.Features.Commands.AppUsers.LoginUser;
using E_CommerceAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {

        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager; // -> bu da ıdentityden gelir sign işlemleri için hizmet veren servis
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserService _userService; // refreshToken yenilemesi için
        public AuthService(IConfiguration configuration, UserManager<AppUser> userManager, ITokenHandler tokenHandler, SignInManager<AppUser> signInManager, IUserService userService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;
        }

        public async Task<Token> GoogleLoginAsync(GoogleAuthenticationDTO model, int accessTokenLifeTime)
        {
            // API Keyimizi gizlemek amaçlı
            string text = File.ReadAllText(_configuration["GoogleAuthenticationAPI"]);
            var dict = JsonConvert.DeserializeObject<APIKeyDTO>(text);
            string googleApiId = dict.web.client_id;


            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { googleApiId }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(model.IdToken, settings);
            // bunların eslesmesi lazım aynı mı gelen id ler

            // identityden gelen aspçnetuserslogin tablosuna bilgileri çekicez
            // not dıs kaynaktak ggelen aspnetuserlogine ayriyetten kayıt olur klasik olan aspnetusers a kayıt olur amaç dısardan kimler geldi bilmek
            //GOOGLE = PROVIDER
            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");

            // infosu verilen kullanıcı bizim aspnetuserLogin da var mı onu kontrol edicez
            AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            bool result = user != null;
            //null gelirse userLogin tablosunda yok bu adam haliyle user tablosunda da yok kayıt etcez ozaman
            if (user == null)
            {
                // ama biz gene de user tablosunda var mı tekrar bakalım
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    // olusturlaım
                    user = new AppUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = payload.Email,
                        Name = model.FirstName,
                        Surname = model.LastName,


                    };
                    // kaydettik
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }

                result = true;
            }

            if (result)
            {
                await _userManager.AddLoginAsync(user, info); //AspNetUsersLogin
            }
            else
            {
                throw new Exception("Invalid External Authentication :((((((");
            }


            Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 300);

            return token;
        }

        public async Task<Token> LoginAsync(InternalLoginDTO model, int accessTokenLifeTime)
        {
            // kullanciyi ariyoruz once useerNmae ya da emaile gore
            AppUser user = await _userManager.FindByNameAsync(model.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);
            }

            if (user == null)
                throw new UserNotFoundException(UserNotFoundException.Message);

            //boolen sonuç döndürür authantica oldun mu oldmadın mı appUserda var mı bu user sifre ona bakar kendi hasler falan o islemleri kendi yapıyor cozuyor
            SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false); // false hatalı durumda kitleme mekanizması

            if (signInResult.Succeeded) // Authentication basarili
            {
                // yetkiler belirlenecek
                // token olusacak
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 300);
                return token;
            }
            else
            {
                throw new UserAuthenticationErrorException(UserAuthenticationErrorException.Message);
            }
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if(user?.RefreshTokenEndDate > DateTime.UtcNow) {

                Token token = _tokenHandler.CreateAccessToken(15, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 300);
                    
                return token;
            }
            else
            {
                throw new UserNotFoundException(UserNotFoundException.Message);

            }

        }
    }
}
