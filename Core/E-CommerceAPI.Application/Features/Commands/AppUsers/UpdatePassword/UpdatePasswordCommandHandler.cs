using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.AppUsers.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
    {
        private readonly IUserService _userService;

        public UpdatePasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            if(!request.Password.Equals(request.PasswordConfirm))
            {
                throw new PasswordChangeFieldException("Lutfen Sifreyi Dogrulayiniz");
            }
            await _userService.UpdatePasswordAsync(request.UserId, request.ResetToken, request.Password);

            return new();
            
        }
    }
}
