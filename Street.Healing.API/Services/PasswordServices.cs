using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;

namespace Street.Healing.API.Services
{
    public class PasswordServices : IPasswordServices
    {

        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        private static readonly int SaltSize = 16;
        private static readonly int HashSize = 20;
        private static readonly int Iterations = 10000;
        public class HashSalt
        {
            public string Hash { get; set; }
            public string Salt { get; set; }
        }

        public static HashSalt GenerateSaltedHash(int size, string password)
        {
            var saltBytes = new byte[size];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);
            var salt = Convert.ToBase64String(saltBytes);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            HashSalt hashSalt = new HashSalt { Hash = hashPassword, Salt = salt };
            return hashSalt;
        }

        /// <summary>
        /// Hash the password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string HashPassword(string password)
        {
            byte[] salt;
            rngCsp.GetBytes(salt = new byte[SaltSize]);

            var key = new Rfc2898DeriveBytes(password, salt, Iterations);
            var hash = key.GetBytes(HashSize);

            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            var base64Hash = Convert.ToBase64String(hashBytes);

            return base64Hash;
        }

        /// <summary>
        /// Verify If the password is in the correct format
        /// </summary>
        /// <param name="password"></param>
        /// <param name="base64Hash"></param>
        /// <returns></returns>
        public  bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
        }
        //public bool VerifyPassword(string password, string base64Hash)
        //{
        //    var hashBytes = Convert.FromBase64String(base64Hash);

        //    var salt = new byte[SaltSize];
        //    Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        //    var key = new Rfc2898DeriveBytes(password, salt, Iterations);
        //    byte[] hash = key.GetBytes(HashSize);

        //    for (var i = 0; i < HashSize; i++)
        //    {
        //        if (hashBytes[i + SaltSize] != hash[i])
        //            return false;
        //    }
        //    return true;
        //}

        /// <summary>
        /// Check if Password respects the norms
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public string CheckPasswordStrength(string pass)
        {
            StringBuilder sb = new StringBuilder();
            if (pass.Length < 9)
                sb.Append("Minimum password length should be 8" + Environment.NewLine);
            if (!(Regex.IsMatch(pass, "[a-z]") && Regex.IsMatch(pass, "[A-Z]") && Regex.IsMatch(pass, "[0-9]")))
                sb.Append("Password should be AlphaNumeric" + Environment.NewLine);
            if (!Regex.IsMatch(pass, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]"))
                sb.Append("Password should contain special character" + Environment.NewLine);
            return sb.ToString();
        }
    }
}
