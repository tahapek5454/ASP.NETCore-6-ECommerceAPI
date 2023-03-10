using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.DTOs.Orders;
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

        public async Task SendCompletedOrderMailAsync(CompletedOrderDTO completedOrderDTO)
        {
            string mail = $"Merhabalar <strong>{completedOrderDTO.UserName} {completedOrderDTO.UserSurname} </strong>. <br> " +
                $"<strong> {completedOrderDTO.OrderDate} </strong> tarihinde vermis oldugunuz <strong> {completedOrderDTO.OrderCode} </strong> kodlu siparisiniz kargo firmasina verilmistir. <br>" +
                $"Sizlere iyi gunler dileriz. <br>" +
                $"<strong> ECommerce - created by Taha Pek </strong>" +
                $"<strong> Egitim Amacli Yapilmis Bir Deneme Projesidir. Itibat Etmeyiniz. </strong>";

            await SendMessageAsync(completedOrderDTO.Email, $"{completedOrderDTO.OrderCode} | kodlu Siparis", mail);


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

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder mail = new();

            mail.AppendLine("<strong>Merhabalar</strong> <br> Eger bir sifre talebinde bulunduysaniz lutfen assagidaki linke tiklayiniz aksi halde maili dikkate almayiniz." +
                " <br> <strong>");
            mail.AppendLine("<a target = \"_blank\" href=\""+ _configuration["AngularClientUrl"] + "/update-password/"+ userId + "/"+ resetToken + "\""+ ">");
            mail.AppendLine("Yeni sifre talebi için tiklayiniz. </a> </strong>");
            mail.AppendLine("<br><br>");
            mail.AppendLine("<strong>Saygılarımla... E-Commerce Created By Taha Pek </strong>");

         
            await SendMessageAsync(to, "Sifre Yenileme Islemi", mail.ToString());


        }
    }
}
