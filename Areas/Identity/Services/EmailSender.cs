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
        public async Task SendEmailAsync(string email , string subject , string masssege)
        {
            using(var Client = new SmtpClient())
            {
                var Credential = new NetworkCredential()
                {
                    UserName = "lninomls9",
                    Password = "0925751499Al@"
                };
                Client.Credentials = Credential;
                Client.Host = "smtp.gmail.com";
                Client.Port = 587;
                Client.EnableSsl = true;
                using(var emailMassege = new MailMessage())
                {
                    emailMassege.To.Add(new MailAddress(email));
                    emailMassege.From = new MailAddress("lninomls9@gmail.com");
                    emailMassege.Subject = subject;
                    emailMassege.IsBodyHtml = true;
                    emailMassege.Body = masssege;
                    Client.Send(emailMassege);
                };

                await Task.CompletedTask;
                
            }
        }
    }
}
