using System.Security.Cryptography;

namespace Street.Healing.Business.Tech.Helpers
{
    public static class PasswordHash
    {
        public static Tuple<string, string> GenerateSaltedHash(int size, string password)
        {
            var saltBytes = new byte[size];
            var provider = RandomNumberGenerator.Create();
            provider.GetNonZeroBytes(saltBytes);
            var salt = Convert.ToBase64String(saltBytes);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA1);
            var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            return new Tuple<string, string>(hashPassword, salt);
        }
    }
}
