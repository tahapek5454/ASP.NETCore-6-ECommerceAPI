using E_CommerceAPI.Application.Abstractions.Services.Configurations;
using E_CommerceAPI.Application.CustomAttributes;
using E_CommerceAPI.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class ApplicationServicesController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationServicesController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionTypes.Reading, Definition = "Get Authorize Definitions EndPoints", Menu ="Application Services")]
        public IActionResult GetAuthorizeDefinitionEndPoints()
        {
            var datas = _applicationService.GetAuthorizeDefinitonEndPoints(typeof(Program));
            return Ok(datas);
        }
    }
}
