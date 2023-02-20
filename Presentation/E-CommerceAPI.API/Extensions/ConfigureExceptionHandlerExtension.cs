using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace E_CommerceAPI.API.Extensions
{
    static public class ConfigureExceptionHandlerExtension
    {
        public static void ConfigureExceptionHandler<T>(this WebApplication webApplication, ILogger<T> logger)
        {
            webApplication.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType= "application/json";

                    // hata oldugundan bilgiyi bu yapılanma verir
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        // loglama yapılabilir
                        logger.LogError(contextFeature.Error.Message);
                        // donukecek cevap
                        await context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Title = "!!!! Hata Alindi !!!!"
                        }));
                    }
                });
            });

        }
    }
}
