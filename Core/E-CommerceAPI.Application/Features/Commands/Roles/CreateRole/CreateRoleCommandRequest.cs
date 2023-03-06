using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.Roles.CreateRole
{
    public class CreateRoleCommandRequest: IRequest<CreateRoleCommandResponse>
    {
        public string Name { get; set; }
    }
}
