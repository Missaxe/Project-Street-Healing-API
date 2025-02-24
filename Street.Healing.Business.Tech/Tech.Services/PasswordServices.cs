using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;

namespace Street.Healing.API.Services
{
    public class PasswordServices : IPasswordServices
    {
        private static readonly char[] chars =
          "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();

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
        /// Create snet token 
        /// </summary>
        /// <returns></returns>
        public string CreateJwt()
        {
            using var rng = RandomNumberGenerator.Create();
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
}
