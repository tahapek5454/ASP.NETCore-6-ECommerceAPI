using E_CommerceAPI.Application.Abstractions.Services.Authentications;
using E_CommerceAPI.Application.Abstractions.Tokens;
using E_CommerceAPI.Application.DTOs;
using E_CommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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
        private readonly IExternalAuthenticationService _externalAuthenticationService;

        public GoogleLoginCommandHandler(IExternalAuthenticationService externalAuthenticationService)
        {
            _externalAuthenticationService = externalAuthenticationService;

        }
        

        public async  Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            Token token = await _externalAuthenticationService.GoogleLoginAsync(new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                IdToken = request.IdToken
            }, 900);

            return new()
            {
                Token = token,
            };


        }
    }
}
