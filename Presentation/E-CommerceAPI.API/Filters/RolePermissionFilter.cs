using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.CustomAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace E_CommerceAPI.API.Filters
{
    public class RolePermissionFilter : IAsyncActionFilter
    {
        // bize actionFilter olusturmamaizi saglar

        private readonly IUserService _userService;

        public RolePermissionFilter(IUserService userService)
        {
            _userService = userService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            // contexten biz httpConteximize ulasilip ilgili istegin bilgilerini elde edebeilirz
            // daha oneden olusturdugumux iidentity kalibinde usrnamei vermistik programccs de hani
            //oralara ersisicez
            var userName = context.HttpContext.User.Identity?.Name;
            // taharesma admin
            if(!string.IsNullOrEmpty(userName) &&  userName != "taharesma")
            {
                // istek hangi actiona giderse onla ilgili bilgileri yakalamak için
                var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

                // actionun attribute bilgilerini yakalamaltiz
                // tanımlamis oldugumuz attributun tipini verdik bize bilgilierini getirecek simdi
                // Not gelen nesne attribute turunde olacak biz ona sen kesin autrhorizeDefinitonAtrribute donder dicez ki
                // ilgili propertylere erisebilelin bir nevi casting yapacagiz
                var attribute = descriptor?.MethodInfo.GetCustomAttribute(typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
                // bizim httpAtrribbutlarına da ihtiyacimiz olacak oradan http typelarını elde edicez(get, post, put, delete)
                var httpAttribute = descriptor?.MethodInfo.GetCustomAttribute(typeof(HttpMethodAttribute)) as HttpMethodAttribute;

                if (attribute != null)
                {
                    // biz veritabanına ilgili ayrimi yapabailmek için code adında bir kolon olustup deger yazmistik
                    // onla eslesecek mi Diye burda da olustucaz
                    var code = $"{(httpAttribute != null ? httpAttribute.HttpMethods.First() : HttpMethods.Get)}.{attribute.ActionType}.{attribute.Definition.Replace(" ", "")}";

                    var hasRole = await _userService.HasRolePermissionToEndpointAsync(userName, code);

                    if (!hasRole)
                    {
                        // yetkin yok demektir resulta direkt unauthorize yollayalım
                        // resulta mudehale ediyorsan next dememelisin
                        context.Result = new UnauthorizedResult();
                    }
                    else
                    {
                        await next();
                    }

                }
                else
                {
                    await next();
                }
            }
            else
            {
                await next();
            }

           
        }
    }
}
