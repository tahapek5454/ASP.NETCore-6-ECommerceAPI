using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.DTOs.Users;
using E_CommerceAPI.Application.Features.Commands.AppUsers.CreateUser;
using E_CommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
    }
}
