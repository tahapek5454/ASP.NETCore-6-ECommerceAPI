using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.Roles.DeleteRole
{
    public class DeleteRoleCommandRequest: IRequest<DeleteRoleCommandResponse>
    {
        public string Id { get; set; }
    }
}
