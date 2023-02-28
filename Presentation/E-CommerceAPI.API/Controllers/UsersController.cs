using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.Features.Commands.AppUsers.CreateUser;
using E_CommerceAPI.Application.Features.Commands.AppUsers.GoogleLogin;
using E_CommerceAPI.Application.Features.Commands.AppUsers.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMailService _mailService;
        public UsersController(IMediator mediator, IMailService mailService)
        {
            _mediator = mediator;
            _mailService = mailService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse reponse = await _mediator.Send(createUserCommandRequest);
            return Ok(reponse);
        }

        //TEST Amacli

        [HttpGet]
        public async Task<IActionResult> MailExampleTest()
        {
            await _mailService.SendMessageAsync("tahapek5454@gmail.com", "Ornek Mail", "<strong>Selam Nasılsın</strong>");

            return Ok();    

        }


       
    }
}
