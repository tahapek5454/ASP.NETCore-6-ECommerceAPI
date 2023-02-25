using E_CommerceAPI.Application.Abstractions.Hubs;
using E_CommerceAPI.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SignalR.HubServices
{
    public class OrderHubService : IOrderHubService
    {
        private readonly IHubContext<OrderHub> _context;

        public OrderHubService(IHubContext<OrderHub> context)
        {
            _context = context;
        }

        public async Task OrderAddedMessageAsync(string message)
        {
           await _context.Clients.All.SendAsync(ReceiveFunctionName.OrderAddedMessage, message);
        }
    }
}
