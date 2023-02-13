using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application
{
    public static class ServiceRegistration
    {
        public static void AddAplicationServices(this IServiceCollection services)
        {
            //Farklı olarak mediatr servislerini kullanacagimizdan onun depdency Injectionlarından gelen metodu kullanacagiz
            // metod bize bulundugu handler yapılanmaları okuyacak bizim sadece yapılanmaların bulundugu assemplyi istiyor biz de kısa yoldan
            // typeOf(serviceRegisterion) verek oralardaki ilgili verilere gore handlarla okuyup verecektir
            services.AddMediatR(typeof(ServiceRegistration));
        }
    }
}
