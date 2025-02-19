using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;
using Street.Healing.API.Controllers;
using static QRCoder.PayloadGenerator;

namespace Street.Healing.API.Helpers
{
    public  class DataValidators
    {
        private static  ILogger<DataValidators> _logger ;

        private DataValidators(ILogger<DataValidators> logger)
        {
            _logger = logger;

        }
        
        /// <summary>
        /// check if the email's form is valid
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
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
            catch(Exception ex) 
            {
                _logger.LogError(message: $"{ConstMessages.ExceptionIsValidEmail} {ex.Message} ");
                return false;
            }
        }

        /// <summary>
        /// Check if Password respects the norms
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static string IsPasswordValid(string pass)
        {
            StringBuilder sb = new StringBuilder();
            if (pass.Length < 9)
                sb.Append(ConstMessages.MinLenghtPass + Environment.NewLine);
            if (!(Regex.IsMatch(pass, "[a-z]") && Regex.IsMatch(pass, "[A-Z]") && Regex.IsMatch(pass, "[0-9]")))
                sb.Append(ConstMessages.AlphaNumPass + Environment.NewLine);
            if (!Regex.IsMatch(pass, ConstMessages.RegexPass))
                sb.Append(ConstMessages.SpecialCharPass + Environment.NewLine);
            return sb.ToString();
        }

        /// <summary>
        /// Verify If the password is in the correct format
        /// </summary>
        /// <param name="password"></param>
        /// <param name="base64Hash"></param>
        /// <returns></returns>
        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            try
            {
                var saltBytes = Convert.FromBase64String(storedSalt);             
                var rfc2898DeriveBytes =  new Rfc2898DeriveBytes(
                    enteredPassword,
                    saltBytes,
                    10000,
                    HashAlgorithmName.SHA1
                    );
                return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
            }
            catch (Exception ex)
            {
                _logger.LogError(message: $"{ConstMessages.ExceptionVerifyPass} {ex.Message} ");
                throw;
            
            }

        }
    }
}
