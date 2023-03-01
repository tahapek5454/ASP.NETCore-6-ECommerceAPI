using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.AppUsers.VerifyResetToken
{
    public class VerifyResetTokenCommandRequest: IRequest<VerifyResetTokenCommandResponse>
    {
        public string UserId { get; set; }
        public string ResetToken { get; set; }
    }
}
