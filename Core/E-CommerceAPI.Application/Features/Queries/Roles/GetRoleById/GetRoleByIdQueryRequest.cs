using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.Roles.GetRoleById
{
    public class GetRoleByIdQueryRequest: IRequest<GetRoleByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
