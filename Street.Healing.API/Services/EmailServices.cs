using MimeKit;
using MailKit.Net.Smtp;
using Street.Healing.API.Helpers;
using System.Security.Cryptography;
using System.Text;

namespace Street.Healing.API.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly EmailConfiguration _emailConfig;
        private static readonly char[] chars =
       "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
        public EmailServices(EmailConfiguration emailConfig) => _emailConfig = emailConfig;
        public async Task SendEmailAsync(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            await SendAsync(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

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
            catch
            {
                //log an error message or throw an exception or both.
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }

        public string CreateJwt()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var bytes = new byte[6];
                rng.GetBytes(bytes);

                var result = new StringBuilder(6);
                foreach (var byteValue in bytes)
                {
                    result.Append(chars[byteValue % chars.Length]);
                }

                return result.ToString();
            }
        }

        /// <summary>
        /// check if the email's form is valid
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
