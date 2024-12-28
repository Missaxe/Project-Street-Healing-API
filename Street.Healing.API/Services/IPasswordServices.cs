namespace Street.Healing.API.Services
{
    public interface IPasswordServices
    {
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string base64Hash);
        public string CheckPasswordStrength(string pass);
    }
}
