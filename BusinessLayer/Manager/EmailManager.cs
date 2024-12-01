
using BusinessLayer.Service;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client.Extensibility;
using MimeKit;
using SahredLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Manager
{
    public class EmailManager : IEmailService
    {private readonly MailSettings _mail;
        public EmailManager(IOptions<MailSettings> options)
        {
            _mail = options.Value;
        }
        public async Task<bool> SendMail(string toEmail, string subject, string body)
        {
            try
            {
                SmtpClient client = new SmtpClient(_mail.Host, _mail.Port);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_mail.UserName, _mail.Password);

                // Create email message
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_mail.FromEmail);
                mailMessage.To.Add(toEmail);
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                StringBuilder mailBody = new StringBuilder();
            //    mailBody.AppendFormat("<h1>Forgot Email</h1>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat(body);
                mailBody.AppendFormat("<br />");
            //    mailBody.AppendFormat("<p>Thank you For Registering account</p>");
                mailMessage.Body = mailBody.ToString();

                // Send email
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
