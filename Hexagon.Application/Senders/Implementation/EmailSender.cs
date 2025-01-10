using Hexagon.Application.Senders.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Application.Senders.Implementation
{
    public class EmailSender(IConfiguration configuration) : IEmailSender
    {
        public async Task<bool> Send(string to, string subject, string body)
        {
            var emailSettings = configuration.GetSection("EmailSettings");

            var smtpClient = new SmtpClient(emailSettings["SMTPHost"])
            {
                Port = int.Parse(emailSettings["SMTPPort"]),
                Credentials = new NetworkCredential(emailSettings["SenderEmail"], emailSettings["SenderPassword"]),
                EnableSsl = bool.Parse(emailSettings["UseSSL"])
            };

            MailMessage mailMessage = new MailMessage()
            {
                From = new MailAddress(emailSettings["SenderEmail"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(to);
            await smtpClient.SendMailAsync(mailMessage);

            await Task.CompletedTask;
            return true;
        }
    }
}
