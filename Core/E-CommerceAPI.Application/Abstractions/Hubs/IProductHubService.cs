using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Abstractions.Hubs
{
    public interface IProductHubService
    {
        Task ProductAddedMesageAsync(string message);
    }
}
