using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.AppUsers.GetAllUsers
{
    public class GetAllUsersQueryRequest: IRequest<GetAllUsersQueryResponse>
    {
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
