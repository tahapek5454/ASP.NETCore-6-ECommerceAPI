using E_CommerceAPI.Application.Exceptions;
using E_CommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.AppUsers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            //AspNetCore.Identity bize UserManager adlı bir servis sunar ve iligli işlemleri yapmamıza olanak tanır.
            // Bu manager db kısmında serviceRegistraion yaparken Ioc ye eklenir

            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName= request.UserName,
                Email= request.Email,
                Name= request.Name,
                Surname= request.Surname,
            }, request.Password);

            // passwordu ayri veme sebebimiz haslenerek vermek metod kendi yapıyor

            CreateUserCommandResponse response= new CreateUserCommandResponse() { Success = result.Succeeded};

            if(result.Succeeded)
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
