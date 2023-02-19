using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.DTOs.Users;
using E_CommerceAPI.Application.Exceptions;
using E_CommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.AppUsers.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            // is kurallarının oldugu yerleri Infrastructure katmanına tasıdık
            CreateUserResponseDTO responseDTO = await _userService.CreateAsync(new()
            {
                Email = request.Email,
                Name = request.Name,
                Password = request.Password,
                RePassword = request.RePassword,
                Surname = request.Surname,
                UserName = request.UserName,
            });

            return new()
            {
                Message = responseDTO.Message,
                Success = responseDTO.Success,
            };

        }
    }
}
