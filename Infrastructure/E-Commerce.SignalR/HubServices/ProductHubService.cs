using E_Commerce.SignalR.Hubs;
using E_CommerceAPI.Application.Abstractions.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.SignalR.HubServices
{
    public class ProductHubService : IProductHubService
    {
        // IHubContexi biz serviceRegistration addsignalR diyerek IOc icersine eklmeis olduk ilgili her sey eklendi
        private readonly IHubContext<ProductHub> _context;

        public ProductHubService(IHubContext<ProductHub> context)
        {
            _context = context;
        }

        public async Task ProductAddedMesageAsync(string message)
        {
            // clientlarda ilgili fonksiton tetiklenecek
            // fonksiyon adı, mesaj
            await _context.Clients.All.SendAsync(ReceiveFunctionName.ProductAddedMessage,message);
        }
    }
}
