using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Infrastructure.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        // actiona gelen isteklerde filter yapmamizi sagliyor
        // controllerın kendi filterını deve dısı bırakmıstık simdi burdan kendi filterımızı yazıcaz
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // dogulanmadıysa
            if(!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Any())
                    .ToDictionary(e => e.Key, e => e.Value.Errors.Select(e => e.ErrorMessage))
                    .ToArray();

                context.Result = new BadRequestObjectResult(errors);
                return;

            }
            // bu filter bitti diger filter tetiklensin
            await next();
        }
    }
}
