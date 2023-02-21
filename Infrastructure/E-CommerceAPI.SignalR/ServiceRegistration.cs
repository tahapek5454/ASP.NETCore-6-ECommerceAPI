using E_CommerceAPI.Application.Abstractions.Hubs;
using E_CommerceAPI.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IProductHubService, ProductHubService>();
            serviceCollection.AddSignalR();
        }
    }
}
