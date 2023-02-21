using E_CommerceAPI.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SignalR
{
    public static class HubRegistration
    {
        public static void MapHubs(this WebApplication webApplication)
        {
            // hublarımızın endpointlerini verecegiz
            // birden fazla olabilecegi için webapplicationu extension ettik program.cs i sisirmedik
            webApplication.MapHub<ProductHub>("/product-hub");
        }
    }
}
