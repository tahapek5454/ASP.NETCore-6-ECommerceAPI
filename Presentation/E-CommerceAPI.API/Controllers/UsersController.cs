using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.CustomAttributes;
using E_CommerceAPI.Application.Enums;
using E_CommerceAPI.Application.Features.Commands.AppUsers.AssignRoleToUser;
using E_CommerceAPI.Application.Features.Commands.AppUsers.CreateUser;
using E_CommerceAPI.Application.Features.Commands.AppUsers.GoogleLogin;
using E_CommerceAPI.Application.Features.Commands.AppUsers.LoginUser;
using E_CommerceAPI.Application.Features.Commands.AppUsers.UpdatePassword;
using E_CommerceAPI.Application.Features.Queries.AppUsers.GetAllUsers;
using E_CommerceAPI.Application.Features.Queries.AppUsers.GetRolesToUser;
using E_CommerceAPI.Application.Features.Queries.Roles.GetRoleById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        //[HttpGet]
        //public async Task<IActionResult> MailExampleTest()
        //{
        //    await _mailService.SendMessageAsync("tahapek5454@gmail.com", "Ornek Mail", "<strong>Selam Nasılsın</strong>");

        //    return Ok();    

        //}

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordCommandRequest updatePasswordCommandRequest)
        {
            UpdatePasswordCommandResponse response = await _mediator.Send(updatePasswordCommandRequest);

            return Ok(response);
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes ="Admin")]
        [AuthorizeDefinition(ActionType = ActionTypes.Reading, Definition ="Get All Users", Menu ="Users")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryRequest getAllUsersQueryRequest)
        {
            GetAllUsersQueryResponse response = await _mediator.Send(getAllUsersQueryRequest);
            return Ok(response);
        }


        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionTypes.Writing, Definition = "Assign Role to User", Menu = "Users")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleToUserCommandRequest assignRoleToUserCommandRequest)
        {
            AssignRoleToUserCommandResponse response = await _mediator.Send(assignRoleToUserCommandRequest);

            return Ok(response);

        }



        [HttpGet("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionTypes.Reading, Definition = "Get Roles To Users", Menu = "Users")]
        public async Task<IActionResult> GetRolesToUser([FromRoute] GetRolesToUserQueryRequest getRolesToUserQueryRequest)
        {
            GetRolesToUserQueryResponse response = await _mediator.Send(getRolesToUserQueryRequest);
            return Ok(response);
        }



    }
}
