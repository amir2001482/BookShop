using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BookShop.Areas.Identity.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email,string subject,string message)
        {
            using (var Client = new SmtpClient())
            {
                var Credential = new NetworkCredential
                {
                    UserName="aspcorelearn",
                    Password= "Arz1372Zoo",
                };

                Client.Credentials = Credential;
                Client.Host = "smtp.gmail.com";
                Client.Port = 587;
                Client.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(email));
                    emailMessage.From = new MailAddress("aspcorelearn@gmail.com");
                    emailMessage.Subject = subject;
                    emailMessage.IsBodyHtml = true;
                    emailMessage.Body = message;

                    Client.Send(emailMessage);
                };

                await Task.CompletedTask;
            }
        }
    }
}
