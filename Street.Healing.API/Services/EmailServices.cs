using MimeKit;
using MailKit.Net.Smtp;
using Street.Healing.API.Helpers;
using System.Security.Cryptography;
using System.Text;
using Street.Healing.API.Controllers;

namespace Street.Healing.API.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly ILogger<EmailServices> _logger;

        public EmailServices(EmailConfiguration emailConfig, ILogger<EmailServices> logger)
        {
            _emailConfig = emailConfig;
            _logger = logger;
        }
     
        public async Task SendEmailAsync(Message message)
        {
             var emailMessage = CreateEmailMessage(message);
             await SendAsync(emailMessage);
        }

        /// <summary>
        /// Send Email ASYNC
        /// </summary>
        /// <param name="mailMessage"></param>
        /// <returns></returns>
        private async Task SendAsync(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                await client.SendAsync(mailMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errror during sending OTP email {ex.Message}");
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }

        /// <summary>
        /// Create Email Content
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder();

            if (message.IsHtml)
            {
                // If it's HTML, set the HTML body
                bodyBuilder.HtmlBody = message.Content;
            }
            else
            {
                // Otherwise, set the plain text body
                bodyBuilder.TextBody = message.Content;
            }

            // Set the body once
            emailMessage.Body = bodyBuilder.ToMessageBody();

            return emailMessage;
        }







    }
}
