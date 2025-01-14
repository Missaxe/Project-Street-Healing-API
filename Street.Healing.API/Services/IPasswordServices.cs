namespace Street.Healing.API.Services
{
    public interface IPasswordServices
    {
        public bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt);
        public string CreateJwt();
    }
}
