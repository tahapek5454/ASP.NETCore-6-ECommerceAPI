using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.AppUsers.RefreshTokens
{
    public class RefreshTokenCommandRequest: IRequest<RefrehsTokenCommandResponse>
    {
        public string RefreshToken { get; set; }
    }
}
