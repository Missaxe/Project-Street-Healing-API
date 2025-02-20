using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Street.Healing.DTO.Helpers
{
    internal static class PasswordHash
    {
        internal static Tuple<string, string> GenerateSaltedHash(int size, string password)
        {
            var saltBytes = new byte[size];
            var provider = RandomNumberGenerator.Create();
            provider.GetNonZeroBytes(saltBytes);
            var salt = Convert.ToBase64String(saltBytes);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA1);
            var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            //HashSalt hashSalt = new HashSalt { Hash = hashPassword, Salt = salt };
            return new Tuple<string, string>(hashPassword, salt);
        }
    }
}
