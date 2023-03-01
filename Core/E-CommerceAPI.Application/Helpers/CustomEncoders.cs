using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Helpers
{
    public static class CustomEncoders
    {
        public static string UrlEncode(this string value)
        {
            //tokenda http pratakollerine takılabilecek ozel karakterler olabilir
            // onları guzel cevirmek lazım
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            return WebEncoders.Base64UrlEncode(bytes);
        }

        public static string UrlDecode(this string value)
        {
            // kullanıcı varsa encod ettigimiz islmeleri decode etttik
            byte[] bytes = WebEncoders.Base64UrlDecode(value);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
