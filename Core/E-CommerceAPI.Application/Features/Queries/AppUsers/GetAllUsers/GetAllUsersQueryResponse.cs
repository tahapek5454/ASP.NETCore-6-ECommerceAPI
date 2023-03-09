using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.AppUsers.GetAllUsers
{
    public class GetAllUsersQueryResponse
    {
        public object Users { get; set; }
        public int TotalUsersCount { get; set; }
    }
}
