using E_CommerceAPI.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.CustomAttributes
{
    public class AuthorizeDefinitionAttribute: Attribute
    {
        public string Menu { get; set; }
        public string Definition { get; set; }
        public ActionTypes ActionType { get; set; }
    }
}
