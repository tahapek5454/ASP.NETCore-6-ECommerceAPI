using E_CommerceAPI.Application.Abstractions.Tokens;
using E_CommerceAPI.Application.DTOs;
using E_CommerceAPI.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Infrastructure.Services.Tokens
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token CreateAccessToken(int second, AppUser user)
        {
            Token token = new Token();

            // keyimizi olusturduk
            SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            // sifrelenmis kimligi olusturalım
            SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);

            // token ayarlarını belitritoruz
            token.Expiration = DateTime.UtcNow.AddSeconds(second);
            JwtSecurityToken securityToken = new(
                    audience : _configuration["Token:Audience"],
                    issuer: _configuration["Token:Issuer"],
                    expires: token.Expiration,
                    notBefore: DateTime.UtcNow,
                    signingCredentials: signingCredentials,
                    claims: new List<Claim>() { new(ClaimTypes.Name, user.UserName) }
                
                );

            // token olusturan sınıftan yararlanarak string tipinde donus yapan tokenımı olusturalım
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            token.AccessToken = tokenHandler.WriteToken(securityToken);

            //RefreshToken Olustur
            token.RefreshToken = CreateRefreshToken();

            return token;

        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();        
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
