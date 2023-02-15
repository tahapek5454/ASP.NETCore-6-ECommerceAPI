using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Exceptions
{
    public class UserCreateFailedException : Exception
    {
        public static string Message { get; set; } = "Kullanıcı Olusturulurken Bir Hata Ile Karsilasildi";
        public UserCreateFailedException()
        {
        }

        public UserCreateFailedException(string? message) : base(message)
        {
        }

        public UserCreateFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
