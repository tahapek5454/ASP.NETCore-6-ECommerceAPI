using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Exceptions
{
    public class UserAuthenticationErrorException : Exception
    {
        public static string Message { get; private set; } = "Kimlik Dogrulanamadı";
        public UserAuthenticationErrorException()
        {
        }

        public UserAuthenticationErrorException(string? message) : base(message)
        {
        }

        public UserAuthenticationErrorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
