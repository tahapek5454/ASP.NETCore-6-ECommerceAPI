using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Exceptions
{
    public class PasswordChangeFieldException : Exception
    {
        public static string Message { get; } = "Sifre Guncellenirken Bir Sorun Olustu";
        public PasswordChangeFieldException()
        {
        }

        public PasswordChangeFieldException(string? message) : base(message)
        {
        }

        public PasswordChangeFieldException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
