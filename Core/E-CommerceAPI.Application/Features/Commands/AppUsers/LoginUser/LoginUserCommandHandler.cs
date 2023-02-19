using E_CommerceAPI.Application.Abstractions.Services.Authentications;
using E_CommerceAPI.Application.Abstractions.Tokens;
using E_CommerceAPI.Application.DTOs;
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
        private readonly IInternalAuthenticationService _internalAuthenticationService;

        public LoginUserCommandHandler(IInternalAuthenticationService internalAuthenticationService)
        {
            _internalAuthenticationService = internalAuthenticationService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {

            Token token = await _internalAuthenticationService.LoginAsync(new()
            {
                Password = request.Password,
                UserNameOrEmail = request.UserNameOrEmail,

            }, 15);

            return new LoginUserSuccessCommandResponse()
            {
                Token = token
            };
            
        }
    }
}
