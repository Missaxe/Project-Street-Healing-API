namespace Street.Healing.API.Services
{
    public interface IPasswordServices
    {
        public string HashPassword(string password);
        public bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt);
        public string CheckPasswordStrength(string pass);
    }
}
