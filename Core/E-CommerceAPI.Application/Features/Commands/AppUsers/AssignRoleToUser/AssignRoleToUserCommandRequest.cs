using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.AppUsers.AssignRoleToUser
{
    public class AssignRoleToUserCommandRequest: IRequest<AssignRoleToUserCommandResponse>
    {
        public string Id { get; set; }
        public string[] Roles { get; set; }

    }
}
