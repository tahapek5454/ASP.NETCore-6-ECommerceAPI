using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.CustomAttributes;
using E_CommerceAPI.Application.Enums;
using E_CommerceAPI.Application.Features.Commands.Roles.CreateRole;
using E_CommerceAPI.Application.Features.Commands.Roles.DeleteRole;
using E_CommerceAPI.Application.Features.Commands.Roles.UpdateRole;
using E_CommerceAPI.Application.Features.Queries.Roles.GetRoleById;
using E_CommerceAPI.Application.Features.Queries.Roles.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class RolesController : ControllerBase
    {
        
        private readonly IMediator _mediator;

        public RolesController(IRoleService roleService, IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionTypes.Reading, Definition ="Get Roles", Menu ="Roles")]
        public async Task<IActionResult> GetRoles([FromQuery]GetRolesQueryRequest getRolesQueryRequest)
        {
            GetRolesQueryResponse response = await _mediator.Send(getRolesQueryRequest);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        [AuthorizeDefinition(ActionType = ActionTypes.Reading, Definition = "Get Role By Id", Menu = "Roles")]
        public async Task<IActionResult> GetRoleById([FromRoute]GetRoleByIdQueryRequest getRoleByIdQueryRequest) {

            GetRoleByIdQueryResponse response = await _mediator.Send(getRoleByIdQueryRequest);


            return Ok(response);
        }

        [HttpPost]
        [AuthorizeDefinition(ActionType = ActionTypes.Writing, Definition = "Create Role", Menu = "Roles")]
        public async Task<IActionResult> CreateRole(CreateRoleCommandRequest createRoleCommandRequest)
        {
            CreateRoleCommandResponse response = await _mediator.Send(createRoleCommandRequest);
            return Ok(response);
        }

        [HttpPut("{Id}")]
        [AuthorizeDefinition(ActionType = ActionTypes.Updating, Definition = "Update Role", Menu = "Roles")]
        public async Task<IActionResult> UpdateRole([FromRoute]string Id, [FromBody]UpdateRoleCommandRequest updateRoleCommandRequest)
        {
            updateRoleCommandRequest.Id = Id;
            UpdateRoleCommandResponse response = await _mediator.Send(updateRoleCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [AuthorizeDefinition(ActionType = ActionTypes.Deleting, Definition = "Delete Role", Menu = "Roles")]
        public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommandRequest deleteRoleCommandRequest)
        {
            DeleteRoleCommandResponse response = await _mediator.Send(deleteRoleCommandRequest);

            return Ok(response);
        }
        
    }
}
