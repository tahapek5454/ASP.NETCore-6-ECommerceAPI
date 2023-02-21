using E_Commerce.SignalR.HubServices;
using E_CommerceAPI.Application.Abstractions.Hubs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.SignalR
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
