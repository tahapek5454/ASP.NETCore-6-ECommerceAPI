using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.Features.Commands.AuthorizationEndpoints.AssignRoleEndpoint;
using E_CommerceAPI.Application.Features.Queries.AuthorizationEndpoints.GetRolesToEndpoint;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class AuthorizationEndpointsController : ControllerBase
    {
       
        private readonly IMediator _mediator;

        public AuthorizationEndpointsController(IMediator mediator)
        {
            
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(AssignRoleEndpointCommandRequest assignRoleEndpointCommandRequest)
        {
            assignRoleEndpointCommandRequest.Type = typeof(Program);
            AssignRoleEndpointCommandResponse response = await _mediator.Send(assignRoleEndpointCommandRequest);
            
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetRolesToEndpoint(GetRolesToEndpointQueryRequest getRolesToEndpointQueryRequest)
        {
            GetRolesToEndpointQueryResponse response = await _mediator.Send(getRolesToEndpointQueryRequest);

            return Ok(response);
        }
    }
}
