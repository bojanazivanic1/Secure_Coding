using MailKit.Net.Smtp;
using MimeKit;
using SecureCode.Exceptions;
using SecureCode.Interfaces.IServices;

namespace SecureCode.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmail(string receiver, string subject, string body)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(_configuration["EmailSettings:Name"], _configuration["EmailSettings:Email"]));
                email.To.Add(MailboxAddress.Parse(receiver));
                email.Subject = subject;
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

                var smtp = new SmtpClient();
                await smtp.ConnectAsync(_configuration["EmailSettings:Host"], int.Parse(_configuration["EmailSettings:Port"]!), MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_configuration["EmailSettings:Email"], _configuration["EmailSettings:Password"]);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception exception)
            {
                throw new InternalServerErrorException("We cannot send email right now.\n" + exception);
            }

            return true;
        }
    }
}
