using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.DTOs.Users;
using E_CommerceAPI.Application.Exceptions;
using E_CommerceAPI.Application.Features.Commands.AppUsers.CreateUser;
using E_CommerceAPI.Application.Helpers;
using E_CommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public int TotaUsersCount => _userManager.Users.Count();

        public async Task AssignRoleToUser(string userId, string[] roles)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);

            if(user != null) {

                var userRoles = await _userManager.GetRolesAsync(user);

                await _userManager.RemoveFromRolesAsync(user, userRoles);
                await _userManager.AddToRolesAsync(user, roles);

            }
            else
            {
                throw new UserNotFoundException(UserNotFoundException.Message);
            }
        }

        public async Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO model)
        {
            //AspNetCore.Identity bize UserManager adlı bir servis sunar ve iligli işlemleri yapmamıza olanak tanır.
            // Bu manager db kısmında serviceRegistraion yaparken Ioc ye eklenir

            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname,
            }, model.Password);

            // passwordu ayri veme sebebimiz haslenerek vermek metod kendi yapıyor

            CreateUserResponseDTO response = new CreateUserResponseDTO() { Success = result.Succeeded };

            if (result.Succeeded)
            {
                // if true veri tabanına kayıt yapıldı
                response.Message = "Kullanıcı Eklenmistir.";
                return response;
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    response.Message += $"{error.Code} - {error.Description}   ";
                }

                return response;
                // throw new UserCreateFailedException(UserCreateFailedException.Message);
            }
        }

        public async Task<List<ListUserDTO>> GetAllUsersAsync(int page, int size)
        {
            var query = _userManager.Users;
            List<AppUser> users = null;

            if(page !=-1 && size != -1)
            {
                users = await query.Skip(page * size).Take(size).ToListAsync();
            }
            else
            {
                users = await query.ToListAsync();
            }

            return users.Select(user => new ListUserDTO {
            
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                TwoFactorEnabled = user.TwoFactorEnabled,
                UserName = user.UserName,
            
            }).ToList();
        }

        public async Task<string[]> GetRolesToUserAsync(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);

            if(user != null)
            {
                var result = await _userManager.GetRolesAsync(user);

                return result.ToArray();
            }
            else
            {
                throw new UserNotFoundException(UserNotFoundException.Message);
            }
        }

        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);

            if(user != null)
            {
                resetToken = resetToken.UrlDecode();
                // userManagerdan yararlanarak degistirme islemi
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if(result.Succeeded)
                {
                    // kullanıcıya karsılık gelen security stamp degerini ezip basta belirtilen tokenin tek bir sefer kullanılmasına olanak tanıyoruz
                    // token security stamp degerine gore esleniyor
                    await _userManager.UpdateSecurityStampAsync(user);
                }
                else
                {
                    throw new PasswordChangeFieldException(PasswordChangeFieldException.Message);
                }
            }
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessToken)
        {
            if(user!= null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessToken);
                await _userManager.UpdateAsync(user);
            }
            else
            {
                throw new UserNotFoundException(UserNotFoundException.Message);
            }

        }
    }
}
