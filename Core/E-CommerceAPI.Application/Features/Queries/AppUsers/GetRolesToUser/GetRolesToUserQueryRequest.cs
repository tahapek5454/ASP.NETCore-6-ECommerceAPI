using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.AppUsers.GetRolesToUser
{
    public class GetRolesToUserQueryRequest: IRequest<GetRolesToUserQueryResponse>
    {
        public string Id { get; set; }
    }
}
