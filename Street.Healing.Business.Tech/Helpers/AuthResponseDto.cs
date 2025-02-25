namespace Street.Healing.Business.Tech.Helpers
{
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful;
        public string? ErrorMessage;
        public string? Token;
        public bool Is2StepVerificationRequired;
        public string? Provider;
    }
}
