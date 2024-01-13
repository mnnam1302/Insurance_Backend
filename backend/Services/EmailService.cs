using backend.Helper;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using static System.Net.WebRequestMethods;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using backend.DTO.Email;

namespace backend.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value; 
        }

        public async Task SendEmaiAsync(EmailRequestDTO mailRequest)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress(_emailSettings.Displayname, "insurance_knh@example.com"));
            email.To.Add(MailboxAddress.Parse(mailRequest.Email));
            email.Subject = "OTP code to reset password";

            var builder = new BodyBuilder();
            builder.HtmlBody = $"This is an OTP code to verfy your password: {mailRequest.Message} \n" +
                                $"This OTP will expire 90 seconds.";
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.Email, _emailSettings.Password);

            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
