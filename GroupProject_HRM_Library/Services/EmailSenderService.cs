using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("linhnhatphuongnguyen@gmail.com", "nhatlinh10A12")
            };

            return client.SendMailAsync(
                new MailMessage(from: "linhnhatphuongnguyen@gmail.com",
                                to: email,
                                subject,
                                message
                                ));
        }
    }
}
