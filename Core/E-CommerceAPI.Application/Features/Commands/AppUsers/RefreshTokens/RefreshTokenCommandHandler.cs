using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.AppUsers.RefreshTokens
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommandRequest, RefrehsTokenCommandResponse>
    {
        private readonly IAuthService _authService;

        public RefreshTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RefrehsTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            Token token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);

            return new()
            {
                Token = token,
            };
        }
    }
}
