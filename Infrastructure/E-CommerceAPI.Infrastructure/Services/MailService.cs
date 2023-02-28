using E_CommerceAPI.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new MailMessage();
            // olusturulan objede html var mı
            mail.IsBodyHtml = isBodyHtml;
            // kime gidecegi belirtilmis
            foreach (var to in tos)
            {
                mail.To.Add(to);

            }
            // subject ve bodylerini yazdik
            mail.Subject = subject;
            mail.Body = body;   

            // kimin gonderecegini belirtelim
            mail.From = new MailAddress(_configuration["Mail:UserName"], "E-Commerce", System.Text.Encoding.UTF8);

            // GONDERME ISLEMI
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:UserName"], _configuration["Mail:ApplicationPassword"]);
            smtp.Port = Int32.Parse(_configuration["Mail:Port"]);
            smtp.EnableSsl = true;
            smtp.Host = _configuration["Mail:Host"];
            await smtp.SendMailAsync(mail);

        }

        public async Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
          await SendMessageAsync(new string[] {to}, subject, body, isBodyHtml);
        }

      
    }
}
