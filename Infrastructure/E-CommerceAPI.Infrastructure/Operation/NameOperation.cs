using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Infrastructure.Operation
{
    public static class NameOperation
    {
        public static string CharecterRegulatory(string name)
        {
            return name.Replace('İ', 'I')
                        .Replace('Ü', 'U')
                        .Replace('Ş', 'S')
                        .Replace('Ç', 'C')
                        .Replace('Ö', 'O')
                        .Replace('Ğ', 'G')
                        .Replace('ü', 'u')
                        .Replace('ş', 's')
                        .Replace('ç', 'c')
                        .Replace('ı', 'i')
                        .Replace('ö', 'o')
                        .Replace('ğ', 'g');

           
        }
    }
}
