using E_CommerceAPI.Application.Abstractions.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.SignalR.Hubs
{
    public class ProductHub : Hub, IProductHubService
    {
        public Task ProductAddedMesageAsync(string message)
        {
            throw new NotImplementedException();
        }
    }
}
