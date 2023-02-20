﻿using E_CommerceAPI.Application.Features.Commands.AppUsers.GoogleLogin;
using E_CommerceAPI.Application.Features.Commands.AppUsers.LoginUser;
using E_CommerceAPI.Application.Features.Commands.AppUsers.RefreshTokens;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> LoginUser(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);


            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshToken([FromBody]RefreshTokenCommandRequest refreshTokenCommandRequest)
        {
            RefrehsTokenCommandResponse response = await _mediator.Send(refreshTokenCommandRequest);

            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest)
        {
            GoogleLoginCommandResponse response = await _mediator.Send(googleLoginCommandRequest);

            return Ok(response);
        }
    }
}
