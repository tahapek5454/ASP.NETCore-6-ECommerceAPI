using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.AppUsers.GoogleLogin
{
    public class GoogleLoginCommandRequest: IRequest<GoogleLoginCommandResponse>
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public string IdToken { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string Provider { get; set; } 

    }
}