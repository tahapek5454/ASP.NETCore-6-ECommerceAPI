using E_CommerceAPI.Application.Abstractions.Tokens;
using E_CommerceAPI.Application.DTOs;
using E_CommerceAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.AppUsers.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IConfiguration _configuration;

        public GoogleLoginCommandHandler(UserManager<AppUser> userManager, ITokenHandler tokenHandler, IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _configuration = configuration;
        }

        public async  Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            // API Keyimizi gizlemek amaçlı
            string text = File.ReadAllText(_configuration["GoogleAuthenticationAPI"]);
            var dict = JsonConvert.DeserializeObject<APIKey>(text);
            string googleApiId = dict.web.client_id;
         

            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { googleApiId }
            };
            
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);
            // bunların eslesmesi lazım aynı mı gelen id ler

            // identityden gelen aspçnetuserslogin tablosuna bilgileri çekicez
            // not dıs kaynaktak ggelen aspnetuserlogine ayriyetten kayıt olur klasik olan aspnetusers a kayıt olur amaç dısardan kimler geldi bilmek
            var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);

            // infosu verilen kullanıcı bizim aspnetuserLogin da var mı onu kontrol edicez
            AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            bool result = user != null;
            //null gelirse userLogin tablosunda yok bu adam haliyle user tablosunda da yok kayıt etcez ozaman
            if(user == null)
            {
                // ama biz gene de user tablosunda var mı tekrar bakalım
                user = await _userManager.FindByEmailAsync(payload.Email);
                if(user == null)
                {
                    // olusturlaım
                    user = new AppUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email= payload.Email,
                        UserName = payload.Email,
                        Name= request.FirstName,
                        Surname = request.LastName,
                        

                    };
                    // kaydettik
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }

                result =  true;
            }

            if(result)
            {
                await _userManager.AddLoginAsync(user, info); //AspNetUsersLogin
            }
            else
            {
                throw new Exception("Invalid External Authentication :((((((");
            }


            Token token = _tokenHandler.CreateAccessToken(5);

            return new()
            {
                Token = token,
            };


        }
    }
}
